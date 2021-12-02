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
            dgvUsers.DataSource = response.data;//form load oldu�unda datagridview e liste getirilir
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

                // kay�t yap�lacak

                user savedUser = baseRepository.postUser(user);

                if (savedUser != null && savedUser.id != null)
                {
                    txtId.Text = savedUser.id.ToString();
                    MessageBox.Show("Kullan�c� olu�turuldu");
                }
                else
                {
                    MessageBox.Show("Kullan�c� olu�turulamad�");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvUsers_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {//tabloda �zerine geldi�imiz sat�rdaki verileri ilgili textBoxlara dolduruyoruz
            this.selectedUser = dgvUsers.SelectedRows[0].DataBoundItem as user;
            this.txtId.Text = this.selectedUser.id.ToString();
            this.txtName.Text = this.selectedUser.name.ToString();
            this.txtEmail.Text = this.selectedUser.email.ToString();
            this.txtGender.Text = this.selectedUser.gender.ToString();
            this.txtStatus.Text = this.selectedUser.status.ToString();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // guncelleme yap�lacak

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
                    MessageBox.Show("Kullan�c� g�ncellendi!");
                }
                else
                {
                    MessageBox.Show("Kullan�c� g�ncellenemedi!");
                }

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //silme yap�lacak

            int id = Convert.ToInt32(txtId.Text);
            user user = new user();
            user.id = id;


            if (id != null && id > 0)
            {
                user delUser = baseRepository.deleteUser(user);
                if(delUser.id != null)
                {
                    MessageBox.Show("Kullan�c� silinemedi!");
                }
                else
                {
                    MessageBox.Show("Kullan�c� silindi!");
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