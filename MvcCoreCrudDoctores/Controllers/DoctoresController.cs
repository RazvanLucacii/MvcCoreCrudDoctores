using Microsoft.AspNetCore.Mvc;
using MvcCoreCrudDoctores.Models;
using MvcCoreCrudDoctores.Repositories;

namespace MvcCoreCrudDoctores.Controllers
{
    public class DoctoresController : Controller
    {
        RepositoryDoctores repoDoc;

        public DoctoresController() 
        {
            this.repoDoc = new RepositoryDoctores();
        }

        public async Task<IActionResult> Index()
        {
            List<Doctor> doctores = await this.repoDoc.GetDoctoresAsync();
            return View(doctores);
        }

        public async Task<IActionResult> Detalles(int id)
        {
            Doctor doctor = await this.repoDoc.FindDoctoresAsync(id);
            return View(doctor);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int hospital_cod, string apellido, string especialidad, int salario)
        {
            await this.repoDoc.InsertDoctorAsync(hospital_cod, apellido, especialidad, salario);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Doctor doctor = await this.repoDoc.FindDoctoresAsync(id);
            return View(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Doctor doctor)
        {
            await this.repoDoc.UpdateDoctorAsync(doctor.DOCTOR_NO, doctor.HOSPITAL_NO, doctor.Apellido, doctor.Especialidad, doctor.Salario);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int doctor_no)
        {
            await this.repoDoc.DeleteDoctorAsync(doctor_no);
            return RedirectToAction("Index");
        }

    }
}
