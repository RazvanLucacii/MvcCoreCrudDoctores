﻿namespace MvcCoreCrudDoctores.Models
{
    public class Doctor
    {
        public int DOCTOR_NO { get; set; }
        public int HOSPITAL_NO { get; set; }
        public string Apellido { get; set; }
        public string Especialidad { get; set; }
        public int Salario { get; set; }
    }
}
