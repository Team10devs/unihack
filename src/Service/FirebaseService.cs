using FirebaseAdmin.Auth;
using MedicalAPI.Domain.Entities.User;
using MedicalAPI.Repository.Doctor;
using MedicalAPI.Domain.Enums;
using MedicalAPI.Domain.DTOs.Doctor;

namespace MedicalAPI.Service.Firebase
{
    public class FirebaseService
    {
        private readonly IDoctorRepository _doctorRepository;

        public FirebaseService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }
        
        public async Task<string> RegisterDoctorAsync(DoctorRequest doctorRequest)
        {
            // Creează utilizatorul în Firebase
            var userRecordArgs = new UserRecordArgs()
            {
                Email = doctorRequest.email,
                Password = doctorRequest.password,
                EmailVerified = false
            };

            UserRecord userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(userRecordArgs);

            // Creează obiectul doctor pentru baza de date
            var doctor = new DoctorModel()
            {
                Email = doctorRequest.email,
                Fullname = doctorRequest.fullname,
                Role = UserRole.Doctor,
                Address = doctorRequest.address,
                License = doctorRequest.license,
                Specialization = doctorRequest.specialization
            };

            await _doctorRepository.AddDoctorAsync(doctor);
            
            return userRecord.Uid;
        }
        
        public async Task<UserRecord> VerifyIdTokenAsync(string idToken)
        {
            FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
            string uid = decodedToken.Uid;
            return await FirebaseAuth.DefaultInstance.GetUserAsync(uid);
        }
    }
}