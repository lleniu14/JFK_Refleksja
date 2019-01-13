namespace RefleksjaJFK
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using System.Text;
    using System.Windows.Forms;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Prism;

#pragma warning disable IDE1006 // Naming Styles
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Event receivers.")]
    public partial class Form1 : Form
    {
        private Assembly assembly;
        private bool zgoda3, zgoda4;
        List<MethodInfo> lista = new List<MethodInfo>();
        private int zaznaczenie;

        public Form1()
        {
            this.InitializeComponent();
        }
        //metody do listboxa
        private void dajMetode()
        {
            this.listBox1.Items.Clear();
            lista.Clear();

            foreach (var module in this.assembly.GetModules())
            {
                dajModul(module);
                for (int i = 0; i < lista.Count; i++)
                {
                    this.listBox1.Items.Add(lista[i]);
                }
            }
        }
        private void dajModul(Module module)
        {
            foreach (var type in module.GetTypes())
            {
                wypiszMetode(type);
            }
        }
        private void wypiszMetode(Type type)
        {
            foreach (MethodInfo a in type.GetMethods(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.GetField))
            {
                lista.Add(a);
            }
        }
        //otwieranie menu do wczytania pliku
        private void otwórzToolStripMenuItem_Click(object sender, EventArgs e)
        {

            using (var openAssemblyDialog = new OpenFileDialog())
            {
                openAssemblyDialog.Title = "Wybierz folder";
                openAssemblyDialog.Multiselect = false;
                openAssemblyDialog.Filter = @"dll|*.dll|exe|*.exe";
                openAssemblyDialog.RestoreDirectory = true;
                if (openAssemblyDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                this.assembly = Assembly.LoadFrom(openAssemblyDialog.FileName);

                this.dajMetode();
            }
        }

        //
        //działanie listboxa(zaznaczanie, wyświetlanie parametrów
        //
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox5.Clear();
            if (listBox1.SelectedIndex != -1)
            {
                textBox5.Clear();

                zaznaczenie = listBox1.SelectedIndex;

                for (int i = 0; i < lista.Count; i++)
                {
                    if (zaznaczenie == i)
                    {
                        wyluskiwacz(lista[i]);
                    }
                }
            }
        }
        private void wyluskiwacz(MethodInfo metoda)
        {
            textBox5.Clear();
            ParameterInfo[] argumenty;
            argumenty = metoda.GetParameters();


            textBox1.Text = argumenty[0].ToString();
            textBox6.Text = argumenty[1].ToString();

            foreach (Attribute a in metoda.GetCustomAttributes(true))
            {
                OpisAtrybutu di = a as OpisAtrybutu;
                if (di != null)
                {
                    textBox5.Text = di.opis.ToString();
                }
            }


        }
        private void wykonajDzialanie(MethodInfo metoda)
        {
            wynikBox.Text = "";

            object wynik = null;
            object classInstance = Activator.CreateInstance(metoda.ReflectedType, false);

            var A = Int32.Parse(textBox3.Text);
            var B = Int32.Parse(textBox4.Text);

            object[] wartosciCzytane = new object[] { A, B };

            wynik = metoda.Invoke(classInstance, wartosciCzytane);

            wynikBox.Text = wynik.ToString();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            komunikatyBox.Text = "";
            if (listBox1.SelectedIndex != -1)
            {
                zaznaczenie = listBox1.SelectedIndex;
                for (int i = 0; i < lista.Count; i++)
                {
                    if (zaznaczenie == i)
                    {
                        if (textBox3.Text != "" && textBox4.Text != "")
                        {
                            if (zgoda3 && zgoda4 == true)
                            {
                                komunikatyBox.Text = "";
                                wykonajDzialanie(lista[i]);
                            }
                            else
                            {
                                komunikatyBox.Text = "";
                                komunikatyBox.Text = " Wprowadź poprawne argumenty!";
                            }
                        }
                        else
                            komunikatyBox.Text = "Podaj argumenty!";
                    }
                }
            }
            else
            {
                komunikatyBox.Text = "Wybierz bibliotekę";
            }
        }
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox5.Clear();
            if (listBox1.SelectedIndex != -1)
            {
                textBox5.Clear();

                zaznaczenie = listBox1.SelectedIndex;

                for (int i = 0; i < lista.Count; i++)
                {
                    if (zaznaczenie == i)
                    {
                        wyluskiwacz(lista[i]);
                    }

                }
            }
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            zgoda3 = true;
            komunikatyBox.Text = "";
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                komunikatyBox.Text = "";
                zgoda3 = false;
                komunikatyBox.Text = "Wartość musi być liczbą całkowitą";
            }
        }



        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            zgoda4 = true;
            komunikatyBox.Text = "";
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                komunikatyBox.Text = "";
                zgoda4 = false;
                komunikatyBox.Text = "Wartość musi być liczbą całkowitą";
            }
        }
    }
}
