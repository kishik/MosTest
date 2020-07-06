using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Text.Json;
using System.Threading;

namespace MosTest
{


    

    public partial class First : Form
    {
        public int page = 0;
        public List<string> data = new List<string>();
        string file;
        bool cancel = false;
        public First()
        {
            InitializeComponent();

        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            ReadAsync();

            //// Configure open file dialog box
            //OpenFileDialog dlg = new OpenFileDialog();
            //dlg.FileName = ""; // Default file name
            //dlg.DefaultExt = ".csv"; // Default file extension
            //dlg.Filter = "CSV file (.csv)|*.csv"; // Filter files by extension

            //// Show open file dialog box
            //Nullable<DialogResult> result = dlg.ShowDialog();

            //// Process open file dialog box results
            //if (result.Value != 0)
            //{
            //    List<Person> people = new List<Person>();
            //    // Open document
            //    try
            //    {
            //        data.Clear();
            //        file = dlg.FileName;
            //        data = File.ReadAllLines(file).ToList<string>();
            //        page = 0;
                    
            //        for (int i = page * 10 + 1; i < Math.Min(data.Count, 10 * (page + 1)) + 1; i++)
            //        {
            //            string[] curElement = data[i].Split(',');
            //            people.Add(new Person(curElement));
            //            Console.WriteLine(people[people.Count - 1].client);
            //        }

            //    }

            //    catch
            //    {
            //        Console.WriteLine("Возникло исключение!");
            //    }

            //    listBox1.Items.Clear();

            //    foreach (Person human in people)
            //    {
            //        listBox1.Items.Add(human.id + " " + human.client + " " + human.insert_date);
            //    }
            //}
        }


        void Read(Nullable<DialogResult> result, OpenFileDialog dlg, List<Person> people)
        {
            

            Thread.Sleep(60000);
            if(!cancel)
            {
                // Process open file dialog box results
                if (result.Value != 0)
                {
                    
                    // Open document
                    try
                    {
                        data.Clear();
                        file = dlg.FileName;
                        data = File.ReadAllLines(file).ToList<string>();
                        page = 0;

                        for (int i = page * 10 + 1; i < Math.Min(data.Count, 10 * (page + 1)) + 1; i++)
                        {
                            string[] curElement = data[i].Split(',');
                            people.Add(new Person(curElement));
                            Console.WriteLine(people[people.Count - 1].client);
                        }

                    }

                    catch
                    {
                        Console.WriteLine("Возникло исключение!");
                    }
  
                }
            }
            
        }
        // определение асинхронного метода
        async void ReadAsync()
        {
            List<Person> people = new List<Person>();
            // Configure open file dialog box
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = ""; // Default file name
            dlg.DefaultExt = ".csv"; // Default file extension
            dlg.Filter = "CSV file (.csv)|*.csv"; // Filter files by extension
            listBox1.Items.Clear();
            // Show open file dialog box
            Nullable<DialogResult> result = dlg.ShowDialog();
            await Task.Run(() => Read(result, dlg, people));                // выполняется асинхронно
            foreach (Person human in people)
            {
                listBox1.Items.Add(human.id + " " + human.client + " " + human.insert_date);
            }
            cancel = false;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if(data.Count > 0 && data[0] != "" && page > 0)
            {
                page--;
                update();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (data.Count > 0 && data[0] != "" && 10 * (page + 1) + 2 < data.Count)
            {
                page++;
                update();
                
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            
            int index = this.listBox1.IndexFromPoint(e.Location);
            if (listBox1.SelectedItem != null)
            {
                //MessageBox.Show(listBox1.SelectedItem.ToString());
                //this.Hide();
                Detalized detalizedForm = new Detalized(this, listBox1.SelectedItem.ToString());
                detalizedForm.Show();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            char[] delimiter = " ".ToCharArray();
            if (listBox1.SelectedItem != null)
            {
                string[] elements = listBox1.SelectedItem.ToString().Split();

                for (int i = page * 10 + 1; i < Math.Min(data.Count, 10 * (page + 1) + 1); i++)
                {
                    if(data[i].StartsWith(elements[0]))
                    {
                        data.RemoveAt(i);
                    }
                }
                Console.WriteLine(data.Count);
                listBox1.Items.Remove(listBox1.SelectedItem.ToString());
                
            }
        }

        public void update()
        {
            List<Person> people = new List<Person>();
            for (int i = page * 10 + 1; i < Math.Min(data.Count, 10 * (page + 1) + 1); i++)
            {
                string[] curElement = data[i].Split(',');
                people.Add(new Person(curElement));
                Console.WriteLine(people[people.Count - 1].client);
            }

            listBox1.Items.Clear();

            foreach (Person human in people)
            {
                listBox1.Items.Add(human.id + " " + human.client + " " + human.insert_date);
            }
        }

        public void writeInfo()
        {
            try
            {
                File.WriteAllLines(file, data);

            }

            catch
            {
                Console.WriteLine("Возникло исключение!");
            }
        }
        



        //adding
        private void button5_Click(object sender, EventArgs e)
        {
            Detalized detalizedForm = new Detalized(this);
            detalizedForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cancel = true;
        }

        //private void listBox1_MouseDoubleClick2(object sender, MouseEventArgs e)
        //{


        //    int index = this.listBox1.IndexFromPoint(e.Location);
        //    if (listBox1.SelectedItem != null)
        //    {
        //        MessageBox.Show(listBox1.SelectedItem.ToString());
        //    }
        //}
    }

    enum Operation
    {
        Adding = 1,   
        Changing = 2
    }

    public class Person
    {
        public Person(string id, string client, string insert_date)
        {
            this.id = Int32.Parse(id);
            this.client = client;
            this.insert_date = DateTime.Parse(insert_date);
        }

        public Person(string[] input)
        {
            this.id = Int32.Parse(input[0]);
            this.client = input[1];
            this.insert_date = DateTime.Parse(input[2]);
        }

        public int id { get; set; }
        public string client { get; set; }
        public DateTime insert_date { get; set; }

    }

}
