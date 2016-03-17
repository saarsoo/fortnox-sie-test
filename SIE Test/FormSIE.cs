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
using FortnoxAPILibrary;
using FortnoxAPILibrary.Connectors;

namespace SIE_Test
{
    public partial class FormSIETest : Form
    {
        public FormSIETest()
        {
            InitializeComponent();
        }

        private void FormSIETest_Load(object sender, EventArgs e)
        {
            tbHost.Text = ConnectionCredentials.FortnoxAPIServer;
        }

        private void bExport_Click(object sender, EventArgs e)
        {
            ConnectionCredentials.FortnoxAPIServer = tbHost.Text;
            ConnectionCredentials.AccessToken = tbAccessToken.Text;
            ConnectionCredentials.ClientSecret = tbClientSecret.Text;

            var connector = new SIEConnector();

            try
            {
                connector.ExportSIE(SIEConnector.SIEType.SIE4, "test.sie");

                if (connector.HasError)
                {
                    throw new Exception(connector.Error.Message);
                }

                using (var reader = new StreamReader("test.sie", Encoding.GetEncoding(437)))
                {
                    rtbSIEData.Text = reader.ReadToEnd();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Misslyckades:\n\n" + exception.Message, "");
            }
        }
    }
}
