using muna.models;
using muna.repositories;

namespace muna
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            baseRepository baseRepository = new baseRepository();
            response response = baseRepository.getUsers();
            dgvUsers.DataSource = response.data;
        }
    }
}