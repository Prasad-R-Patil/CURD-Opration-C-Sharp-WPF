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
            nametxt.Clear();
            agetxt.Clear();
            gendertxt.Clear();
            citytxt.Clear();
        }

        public void LoadGrid()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM FirstTable",con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            datagrid.ItemsSource = dt.DefaultView;
        }
        private void clearbtn_Click(object sender, RoutedEventArgs e)
        {
            ClearData();
        }

        public bool isValid()
        {
            if (nametxt.Text == null) 
            {
                MessageBox.Show("Name is Required" , "Failed" , MessageBoxButton.OK , MessageBoxImage.Error);
                return false;
            }
            if (agetxt.Text != null) 
            {
                MessageBox.Show("Age is Required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (gendertxt.Text != null) 
            {
                MessageBox.Show("Gender is Required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (citytxt.Text != null) 
            { 
                MessageBox.Show("City Name is Required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
       
        private void insertbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                    SqlCommand cmd = new SqlCommand("INSERT INTO FirstTable VALUES (@Name , @Age , @Gender , @City)", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Name", nametxt.Text);
                    cmd.Parameters.AddWithValue("@Age", int.Parse(agetxt.Text));
                    cmd.Parameters.AddWithValue("@Gender", gendertxt.Text);
                    cmd.Parameters.AddWithValue("@City", citytxt.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    LoadGrid();

                    MessageBox.Show("Successfully Inserted", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);

                    ClearData();
                
                
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            {

            }
            
                
        }
    }
}