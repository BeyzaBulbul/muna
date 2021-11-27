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
        baseRepository baseRepository = new baseRepository();
        user selectedUser = new user();
        private void MainForm_Load(object sender, EventArgs e)
        {

            response response = baseRepository.getUsers();
            dgvUsers.DataSource = response.data;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int? id = Convert.ToInt32(txtId.Text);
            user user = new user();
            user.email = txtEmail.Text;
            user.status = txtStatus.Text;
            user.name = txtName.Text;
            user.gender = txtGender.Text;
            user.id = id;
            if (id != null && id > 0)
            {
                // guncelleme yapýlacak

            }
            else
            {
                // kayýt yapýlacak

              
                user savedUser = baseRepository.postUser(user);

                if (savedUser != null && savedUser.id != null)
                {
                    txtId.Text = savedUser.id.ToString();
                    MessageBox.Show("Kullanýcý oluþturuldu");
                }
                else
                {
                    MessageBox.Show("Kullanýcý oluþturulamadý");
                }
            }
        }

        private void dgvUsers_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
          this.selectedUser =  dgvUsers.SelectedRows[0].DataBoundItem as user;
            this.txtId.Text = this.selectedUser.id.ToString();
            this.txtName.Text = this.selectedUser.name.ToString();
            this.txtEmail.Text = this.selectedUser.email.ToString();
            this.txtGender.Text = this.selectedUser.gender.ToString();
            this.txtStatus.Text = this.selectedUser.status.ToString();

        }
    }
}