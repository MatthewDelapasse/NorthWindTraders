using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace NorthWindTraders
{
    public partial class frmCustomers : Form
    {
        public frmCustomers()
        {
            InitializeComponent();
        }

        SqlConnection NWindConnection;
        SqlCommand customersCommand;
        SqlDataAdapter customersAdapter;
        DataTable customersTable;
        CurrencyManager customersManager;

        private void frmCustomers_Load(object sender, EventArgs e)
        {
            //Connect to NWind database
            string filename = Path.GetFullPath("SQLNwindDB.mdf");
            NWindConnection = new SqlConnection("Data Source=.\\SQLEXPRESS01; AttachDbFileName=" + filename + "; Integrated Security=True; Connect Timeout = 30; User Instance = True");

            //open the connection
            NWindConnection.Open();

            //establish command object
            customersCommand = new SqlCommand("SELECT * FROM Customers", NWindConnection);

            //establish data adapter/data table
            customersAdapter = new SqlDataAdapter();
            customersAdapter.SelectCommand = customersCommand;
            customersTable = new DataTable();
            customersAdapter.Fill(customersTable);

            //bind controls to data table
            txtCustomerID.DataBindings.Add("Text", customersTable, "CustomerID");
            txtCompanyName.DataBindings.Add("Text", customersTable, "CompanyName");
            txtContactName.DataBindings.Add("Text", customersTable, "ContactName");
            txtContactTitle.DataBindings.Add("Text", customersTable, "ContactTitle");

            //establish currency managers
            customersManager = (CurrencyManager)BindingContext[customersTable];

            //close the connection
            NWindConnection.Close();

            //dispose of the connection object
            NWindConnection.Dispose();
            customersCommand.Dispose();
            customersAdapter.Dispose();
            customersTable.Dispose();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            customersManager.Position = 0;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            customersManager.Position--;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            customersManager.Position++;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            customersManager.Position = customersManager.Count - 1;
        }
    }
}
