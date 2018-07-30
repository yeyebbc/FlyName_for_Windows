using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace appfilte_to_flyme_theme_icons
{
    public partial class main_form : Form
    {
        public main_form()
        {
            InitializeComponent();
        }

        string path_appfilter;
        string path_drawable;
        string path_output;

        private void btn_locate_appfilter_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog0 = new OpenFileDialog();
            openFileDialog0.Filter = "Appfilter file|*.xml";
            openFileDialog0.Title = "Select an appfilter file";


            if(openFileDialog0.ShowDialog()== System.Windows.Forms.DialogResult.OK)
            {
                path_appfilter = openFileDialog0.FileName;

                txt_box_appfilter.Text = path_appfilter;

                if(txt_box_appfilter.Text!= "The appfilter.xml file")
                {
                    txt_box_appfilter.Font = new Font("Microsoft YaHei UI", txt_box_appfilter.Font.Size, txt_box_appfilter.Font.Style ^ FontStyle.Italic);
                    txt_box_appfilter.ForeColor = Color.Black;
                }

                //System.IO.StreamReader sr = new System.IO.StreamReader(path_appfilter);
            }
        }

        private void btn_drawable_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog0 = new FolderBrowserDialog();
            folderBrowserDialog0.ShowNewFolderButton = false;
            folderBrowserDialog0.Description = "Select the folder where your icons are";
            
            if(folderBrowserDialog0.ShowDialog()==DialogResult.OK)
            {
                path_drawable = folderBrowserDialog0.SelectedPath;
                
                txt_drawable.Text = path_drawable;

                if(txt_drawable.Text!= "The folder where your icons are")
                {
                    txt_drawable.Font = new Font("Microsoft YaHei UI", txt_drawable.Font.Size, txt_drawable.Font.Style ^ FontStyle.Italic);
                    txt_drawable.ForeColor = Color.Black;
                }
            }
        }

        private void btn_output_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.ShowNewFolderButton = true;
            folderBrowserDialog1.Description = "Select or create the folder to save output icons.";

            if(folderBrowserDialog1.ShowDialog()==DialogResult.OK)
            {
                path_output = folderBrowserDialog1.SelectedPath;
                
                txt_output.Text = path_output;

                if(txt_output.Text!= "The folder to save output icons")
                {
                    txt_output.Font = new Font("Microsoft YaHei UI", txt_drawable.Font.Size, txt_drawable.Font.Style ^ FontStyle.Italic);
                    txt_output.ForeColor = Color.Black;
                }
            }
        }
        
        private void txt_box_appfilter_MouseClick(object sender, MouseEventArgs e)
        {
            if (txt_box_appfilter.Text == "The appfilter.xml file") 
            {
                txt_box_appfilter.Clear();
            }
        }

        private void txt_drawable_MouseClick_1(object sender, MouseEventArgs e)
        {
            if (txt_drawable.Text == "The folder where your icons are")
            {
                txt_drawable.Clear();
            }

        }
        private void txt_output_MouseClick(object sender, MouseEventArgs e)
        {
            if (txt_output.Text == "The folder to save output icons")
            {
                txt_output.Clear();
            }
        }

        private void txt_box_appfilter_Leave(object sender, EventArgs e)
        {
            if (txt_box_appfilter.Text == "")
            {
                txt_box_appfilter.Font = new Font("Microsoft YaHei UI Light", txt_box_appfilter.Font.Size, txt_box_appfilter.Font.Style | FontStyle.Italic);
                txt_box_appfilter.ForeColor = Color.Gray;

                txt_box_appfilter.Text = "The appfilter.xml file";
            }
        }

        private void txt_drawable_Leave(object sender, EventArgs e)
        {
            if (txt_drawable.Text == "")
            {
                txt_drawable.Font = new Font("Microsoft YaHei UI Light", txt_drawable.Font.Size, txt_drawable.Font.Style | FontStyle.Italic);
                txt_drawable.ForeColor = Color.Gray;

                txt_drawable.Text = "The folder where your icons are";
            }
        }

        private void txt_output_Leave(object sender, EventArgs e)
        {
            if (txt_output.Text == "")
            {
                txt_output.Font = new Font("Microsoft YaHei UI Light", txt_output.Font.Size, txt_output.Font.Style | FontStyle.Italic);
                txt_output.ForeColor = Color.Gray;

                txt_output.Text = "The folder to save output icons";
            }
        }

        private void btn_start_Click(object sender, EventArgs e)
        {

            this.Cursor = System.Windows.Forms.Cursors.AppStarting;

            if (txt_box_appfilter.Text == "" || txt_box_appfilter.Text == "The appfilter.xml file")
            {
                MessageBox.Show("You must select a valid appfilter file.", "Warning");
                txt_box_appfilter.Focus();
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }

            else if (txt_drawable.Text == "" || txt_drawable.Text == "The folder where your icons are")
            {
                MessageBox.Show("You must specify a valid icons location.", "Warning");
                txt_drawable.Focus();
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }

            else if (txt_output.Text == "" || txt_output.Text == "The folder to save output icons")
            {
                MessageBox.Show("You must specify a valid folder location to save output icons.", "Warning");
                txt_output.Focus();
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }

            else
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(path_appfilter);
                while (sr.Peek() >= 0)
                {
                    string a = "ComponentInfo";
                    string m = sr.ReadLine().Trim();
                    int i = m.IndexOf(a); //Get index of "ComponentInfo"

                    string file_name;
                    string target_file;


                    if (i != -1)
                    {

                        //Cut from "ComponentInfo{" to the end of the line
                        //And trim it
                        string n = m.Substring(i + a.Length + 1);
                        n.Trim();
                        
                        //Get the index of the first "/" in the line that has been cut
                        int ii = n.IndexOf("/");
                        string target_flyme_icon_name;
                        if (ii != -1 && n.ElementAt(ii+1)!='>')
                        {
                            //Retrieve package name from line
                            string flyme_icon_name = n.Substring(0, n.Length - (n.Length - ii)) + ".png";
                            target_flyme_icon_name = System.IO.Path.Combine(path_output, flyme_icon_name);
                        }
                        else
                        {
                            continue;
                        }
                        //Get the index of the first "drawable=" from line
                        int index_drawable = m.IndexOf("drawable");
                        string mid_str_drawable;
                        int mid_cut;
                        string drawable_icon_name;
                        
                        
                        if (index_drawable != -1)
                        {
                            mid_str_drawable = m.Substring(index_drawable + "drawable".Length + 2);
                            mid_str_drawable.Trim();
                            mid_cut = mid_str_drawable.IndexOf("\"");

                            drawable_icon_name = mid_str_drawable.Substring(0, mid_str_drawable.Length - (mid_str_drawable.Length - mid_cut));
                            drawable_icon_name.Trim();

                            //Copy original icon to Output folder.
                            file_name = drawable_icon_name + ".png";
                            string source_file = System.IO.Path.Combine(path_drawable, file_name);
                            target_file = System.IO.Path.Combine(path_output, file_name);

                            if(System.IO.File.Exists(source_file))
                            {
                                //System.IO.File.Copy(source_file, target_file, true);
                                System.IO.File.Copy(source_file, target_flyme_icon_name, true);


                                //Then rename it to match Flyme theme icons.
                                /*if (System.IO.File.Exists(target_flyme_icon_name))
                                {
                                    System.IO.File.Delete(target_flyme_icon_name);
                                }
                                else
                                {
                                    System.IO.File.Move(target_file, target_flyme_icon_name);
                                }*/
                            }
                            
                            

                            //MessageBox.Show(drawable_icon_name);
                        }
                        else
                        {
                            string mm=sr.ReadLine().Trim();
                            //Get the index of the first "drawable=" from line
                            int index_drawable_newline = mm.IndexOf("drawable");
                            string mid_str_drawable_newline;
                            int mid_cut_newline;
                            string drawable_icon_name_newline;

                            mid_str_drawable_newline = mm.Substring(index_drawable + "drawable".Length + 2);
                            mid_str_drawable_newline.Trim();
                            mid_cut_newline = mid_str_drawable_newline.IndexOf("\"");

                            drawable_icon_name_newline = mid_str_drawable_newline.Substring(0, mid_str_drawable_newline.Length - (mid_str_drawable_newline.Length - mid_cut_newline));
                            drawable_icon_name_newline.Trim();

                            //Copy original icon to Output folder.
                            file_name = drawable_icon_name_newline + ".png";
                            string source_file = System.IO.Path.Combine(path_drawable, file_name);
                            target_file = System.IO.Path.Combine(path_output, file_name);

                            if (System.IO.File.Exists(source_file))
                            {
                                //System.IO.File.Copy(source_file, target_file, true);
                                System.IO.File.Copy(source_file, target_flyme_icon_name, true);
                                
                            }
                        }
                    }
                }

                this.Cursor = System.Windows.Forms.Cursors.Default;
                MessageBox.Show("Convert finished.");
            }
        }

    }
}
