using MvcCoreCrudDoctores.Models;
using System.Data;
using System.Data.SqlClient;

namespace MvcCoreCrudDoctores.Repositories
{
    public class RepositoryDoctores
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;

        public RepositoryDoctores() 
        {
            string connectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=MCSD2023";
            this.command = new SqlCommand();
            this.connection = new SqlConnection(connectionString);
            this.command.Connection = this.connection;
        }

        public async Task<List<Doctor>> GetDoctoresAsync()
        {
            string sql = "select * from DOCTOR";
            this.command.CommandText = sql;
            this.command.CommandType = CommandType.Text;
            await this.connection.OpenAsync();
            this.reader = await this.command.ExecuteReaderAsync();
            List<Doctor> doctores = new List<Doctor>();
            while (await this.reader.ReadAsync())
            {
                Doctor doctor = new Doctor();
                doctor.HOSPITAL_NO = int.Parse(this.reader["HOSPITAL_COD"].ToString());
                doctor.DOCTOR_NO = int.Parse(this.reader["DOCTOR_NO"].ToString());
                doctor.Apellido = this.reader["APELLIDO"].ToString();
                doctor.Especialidad = this.reader["ESPECIALIDAD"].ToString();
                doctor.Salario = int.Parse(this.reader["SALARIO"].ToString());
                doctores.Add(doctor);
            }
            await this.reader.CloseAsync();
            await this.connection.CloseAsync();
            return doctores;
        }

        public async Task<Doctor> FindDoctoresAsync(int id)
        {
            string sql = "select * from DOCTOR where DOCTOR_NO=@id";
            this.command.Parameters.AddWithValue("@id", id);
            this.command.CommandText = sql;
            this.command.CommandType = CommandType.Text;
            await this.connection.OpenAsync();
            this.reader = await this.command.ExecuteReaderAsync();
            Doctor doctor = null;
            if (await this.reader.ReadAsync())
            {
                doctor = new Doctor();
                doctor.HOSPITAL_NO = int.Parse(this.reader["HOSPITAL_COD"].ToString());
                doctor.DOCTOR_NO = int.Parse(this.reader["DOCTOR_NO"].ToString());
                doctor.Apellido = this.reader["APELLIDO"].ToString();
                doctor.Especialidad = this.reader["ESPECIALIDAD"].ToString();
                doctor.Salario = int.Parse(this.reader["SALARIO"].ToString());
            }
            else { }
            await this.reader.CloseAsync();
            await this.connection.CloseAsync();
            this.command.Parameters.Clear();
            return doctor;
        }

        public async Task InsertDoctorAsync(int hospital_cod, string apellido, string especialidad, int salario)
        {
            string sql = "SP_INSERTDOCTOR";
            this.command.Parameters.AddWithValue("@HOSPITAL_COD", hospital_cod);
            this.command.Parameters.AddWithValue("@APELLIDO", apellido);
            this.command.Parameters.AddWithValue("@ESPECIALIDAD", especialidad);
            this.command.Parameters.AddWithValue("@SALARIO", salario);
            this.command.CommandText = sql;
            this.command.CommandType = CommandType.StoredProcedure;
            await this.connection.OpenAsync();
            int af = await this.command.ExecuteNonQueryAsync();
            await this.connection.CloseAsync();
            this.command.Parameters.Clear();
            
        }

        public async Task UpdateDoctorAsync(int doctor_no, int hospital_cod, string apellido, string especialidad, int salario)
        {
            string sql = "update DOCTOR set HOSPITAL_COD=@HOSPITAL_COD, APELLIDO=@APELLIDO, ESPECIALIDAD=@ESPECIALIDAD, SALARIO=@SALARIO, DOCTOR_NO =@ID WHERE DOCTOR_NO =@ID ;";
            this.command.Parameters.AddWithValue("@HOSPITAL_COD", hospital_cod);
            this.command.Parameters.AddWithValue("@APELLIDO", apellido);
            this.command.Parameters.AddWithValue("@ESPECIALIDAD", especialidad);
            this.command.Parameters.AddWithValue("@SALARIO", salario);
            this.command.Parameters.AddWithValue("@ID", doctor_no);
            this.command.CommandText = sql;
            this.command.CommandType = CommandType.Text;
            await this.connection.OpenAsync();
            int af = await this.command.ExecuteNonQueryAsync();
            await this.connection.CloseAsync();
            this.command.Parameters.Clear();
        }

        public async Task DeleteDoctorAsync(int id)
        {
            string sql = "delete from DOCTOR where DOCTOR_NO=@id";
            this.command.Parameters.AddWithValue("@id", id);
            this.command.CommandText = sql;
            this.command.CommandType = CommandType.Text;
            await this.connection.OpenAsync();
            int af = await this.command.ExecuteNonQueryAsync();
            await this.connection.CloseAsync();
            this.command.Parameters.Clear();
        }

    }
}
