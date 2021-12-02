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
            dgvUsers.DataSource = response.data;//form load olduðunda datagridview e liste getirilir
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                user user = new user();
                user.email = txtEmail.Text;
                user.status = txtStatus.Text;
                user.name = txtName.Text;
                user.gender = txtGender.Text;

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvUsers_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {//tabloda üzerine geldiðimiz satýrdaki verileri ilgili textBoxlara dolduruyoruz
            this.selectedUser = dgvUsers.SelectedRows[0].DataBoundItem as user;
            this.txtId.Text = this.selectedUser.id.ToString();
            this.txtName.Text = this.selectedUser.name.ToString();
            this.txtEmail.Text = this.selectedUser.email.ToString();
            this.txtGender.Text = this.selectedUser.gender.ToString();
            this.txtStatus.Text = this.selectedUser.status.ToString();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // guncelleme yapýlacak

            int id = Convert.ToInt32(txtId.Text);
            user user = new user();
            user.email = txtEmail.Text;
            user.status = txtStatus.Text;
            user.name = txtName.Text;
            user.gender = txtGender.Text;
            user.id=id;
            if (id != null && id > 0)
            {
                
                user putUser = baseRepository.putUser(user);

                if (putUser != null && putUser.id != null)
                {
                    txtId.Text = putUser.id.ToString();
                    MessageBox.Show("Kullanýcý güncellendi!");
                }
                else
                {
                    MessageBox.Show("Kullanýcý güncellenemedi!");
                }

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //silme yapýlacak

            int id = Convert.ToInt32(txtId.Text);
            user user = new user();
            user.id = id;


            if (id != null && id > 0)
            {
                user delUser = baseRepository.deleteUser(user);
                if(delUser.id != null)
                {
                    MessageBox.Show("Kullanýcý silinemedi!");
                }
                else
                {
                    MessageBox.Show("Kullanýcý silindi!");
                }
            }

        }

        private void btnDBKaydet_Click(object sender, EventArgs e)
        {
            response response = baseRepository.getUsers();
            List<user> aktarilanVeri = response.data;

            if (aktarilanVeri.Count > 0)
                baseRepository.DapperInsert(aktarilanVeri);

        }
    }
}