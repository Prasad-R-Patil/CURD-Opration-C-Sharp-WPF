using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CURD_Opration_C_Sharp_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadGrid();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-3C20DQN\\SQLEXPRESS;Initial Catalog=PersonInfo;Integrated Security=True;Encrypt=False");
        public void ClearData() 
        {
            idtxt.Clear();
            nametxt.Clear();
            agetxt.Clear();
            gendertxt.SelectedIndex = -1;
            citytxt.Clear();
        }

        public void LoadGrid()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM FirstTable",con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = command.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            datagrid.ItemsSource = dt.DefaultView;
        }
        

        
        private void insertbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ( int.Parse(idtxt.Text) != 0 && nametxt.Text != "" && int.Parse(agetxt.Text) != 0 && gendertxt.Text != "" && citytxt.Text != "" )
                {

                    SqlCommand command = new SqlCommand("INSERT INTO FirstTable (ID,Name,Age,Gender,City,InsertDate) VALUES ('"+int.Parse(idtxt.Text)+"' ,'" + nametxt.Text+"' , '"+int.Parse(agetxt.Text)+"' , '"+gendertxt.Text+"' , '"+citytxt.Text+"' , getdate() )", con);
                    con.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Successfullly Inseted.....", "Completed..." , MessageBoxButton.OK , MessageBoxImage.Information);
                    con.Close();
                    LoadGrid();
                    ClearData();
                    
                }
                else
                {
                    if (int.Parse(idtxt.Text) == 0)
                    {
                        MessageBox.Show("Enter Valid ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    if (nametxt.Text == "")
                    {
                        MessageBox.Show("Enter Valid Name", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    if (int.Parse(agetxt.Text) == 0 )
                    {
                        MessageBox.Show("Enter Valid Age", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    if (gendertxt.Text == "")
                    {
                        MessageBox.Show("Select Your Gender", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    if (citytxt.Text == "")
                    {
                        MessageBox.Show("Enter City Name", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }


                }
                
                
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }    
        }

        private void updatebtn_Click(object sender, RoutedEventArgs e)
        {
            if (idtxt.Text != "" && int.Parse(idtxt.Text) != 0)
            {
                
                SqlCommand command = new SqlCommand("UPDATE FirstTable SET Name = '" + nametxt.Text + "' , Age = '" + int.Parse(agetxt.Text) + "' , Gender = '" + gendertxt.Text + "' , City = '" + citytxt.Text + "' , UpdateDate = getdate() where ID = '" + int.Parse(idtxt.Text) + "' ", con);
                con.Open();
                command.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Successfullly Updated.....", "Completed...", MessageBoxButton.OK , MessageBoxImage.Information );
                LoadGrid();
                ClearData();
            }
            else
            {
                MessageBox.Show("Enter Valid Data" , "Error" , MessageBoxButton.OK, MessageBoxImage.Error);
            }



        }

        private void searchbtn_Click(object sender, RoutedEventArgs e)
        {
            if (idtxt.Text != "")
            {
                con.Open();
                SqlCommand command = new SqlCommand("Select * From FirstTable where ID = '" + int.Parse(idtxt.Text) + "'", con);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                datagrid.ItemsSource = dt.DefaultView;
                
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // Fill the fields with the retrieved data
                    nametxt.Text = reader["Name"].ToString();
                    agetxt.Text = reader["Age"].ToString();
                    gendertxt.Text = reader["Gender"].ToString();
                    citytxt.Text = reader["City"].ToString() ;
                }
                else
                {
                    MessageBox.Show("No record found with the given ID.", "No Record", MessageBoxButton.OK , MessageBoxImage.Warning);
                }
                con.Close();
                ClearData();

            }
            else
            {
                MessageBox.Show("Enter Product ID.... ", "Error", MessageBoxButton.OK , MessageBoxImage.Error);
            }
        }

        private void deletebtn_Click(object sender, RoutedEventArgs e)
        {
            if (idtxt.Text != "")
            {
                if (MessageBox.Show("Are you Sure to Delete ? ", "Delete Record", MessageBoxButton.YesNo , MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("DELETE FirstTable where ID = '" + int.Parse(idtxt.Text) + "' ", con);
                    command.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Successfully Deleted...", "Completed..." , MessageBoxButton.OK , MessageBoxImage.Information );
                    LoadGrid();
                    ClearData();
                }
                else
                {
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("Enter ID....", "Error", MessageBoxButton.OK , MessageBoxImage.Error);
            }
        }


        private void clearbtn_Click(object sender, RoutedEventArgs e)
        {
            ClearData();
        }



    }
}