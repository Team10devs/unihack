using FirebaseAdmin.Auth;
using MedicalAPI.Domain.Entities.User;
using MedicalAPI.Repository.Doctor;
using MedicalAPI.Domain.Enums;
using MedicalAPI.Domain.DTOs.Doctor;
using MedicalAPI.Domain.DTOs.Patient;
using MedicalAPI.Repository.Patient;
using MedicalAPI.Repository.User;

namespace MedicalAPI.Service.Firebase
{
    public class FirebaseService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IUserRepository _userRepository;

        public FirebaseService(IDoctorRepository doctorRepository,
            IPatientRepository patientRepository,
            IUserRepository userRepository)
        {
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _userRepository = userRepository;
        }
        
        public async Task<string> RegisterDoctorAsync(DoctorRequest doctorRequest)
        {
            var userRecordArgs = new UserRecordArgs()
            {
                Email = doctorRequest.email,
                Password = doctorRequest.password,
                EmailVerified = false
            };

            UserRecord userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(userRecordArgs);

            
            var doctor = new DoctorModel()
            {
                Id = userRecord.Uid,
                Email = doctorRequest.email,
                Fullname = doctorRequest.fullname,
                Role = UserRole.Doctor,
                Address = doctorRequest.address,
                License = doctorRequest.license,
                Specialization = doctorRequest.specialization,
                Code = GenerateRandomCode(6),
                DeviceToken = ""
            };
    
            await _doctorRepository.AddDoctorAsync(doctor);
            
            return userRecord.Uid;
        }

        public async Task<string> RegisterPatientAsync(PatientRequest patientRequest)
        {
            var userRecordArgs = new UserRecordArgs()
            {
                Email = patientRequest.email,
                Password = patientRequest.password,
                EmailVerified = false
            };

            UserRecord userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(userRecordArgs);

            try
            {
                DoctorModel doctor = null;
                
                if (!string.IsNullOrEmpty(patientRequest.doctorCode))
                {
                    doctor = await _doctorRepository.GetDoctorByCodeAsync(patientRequest.doctorCode);
                    if (doctor == null)
                    {
                        throw new Exception("Doctor not found with the provided code.");
                    }
                }
                
                var patient = new PatientModel()
                {
                    Id = userRecord.Uid,
                    Email = patientRequest.email,
                    Fullname = patientRequest.fullname,
                    Role = UserRole.Patient,
                    BirthDate = patientRequest.birthDate,
                    Gender = patientRequest.gender,
                    DeviceToken = "",
                    MedicalHistory = "",
                    Doctor = doctor 
                };
                
                await _patientRepository.AddPatientAsync(patient);
                
                if (doctor != null)
                {
                    await _patientRepository.AddPatientToDoctorAsync(patientRequest.doctorCode, patient);
                    await _doctorRepository.UpdateDoctorAsync(doctor);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error registering patient: {ex.Message}");
                throw;
            }

            return userRecord.Uid;
        }


        public async Task<string> GetUserTypeAsync(string email)
        {
            var user = await FirebaseAuth.DefaultInstance.GetUserByEmailAsync(email);
            var uid = user.Uid;
            
            var doctor = await _doctorRepository.GetDoctorByIdAsync(uid);
            if (doctor != null)
            {
                return "Doctor";
            }
            
            var patient = await _patientRepository.GetPacientByIdAsync(uid);
            if (patient != null)
            {
                return "Patient";
            }
            
            throw new Exception("User type not found");
        }
        
        public async Task<string> LoginAsync(string email)
        {
            try
            {
                var user = await FirebaseAuth.DefaultInstance.GetUserByEmailAsync(email);
                
                var customToken = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(user.Uid);
                return customToken;
            }
            catch (Exception ex)
            {
                throw new Exception($"Login failed: {ex.Message}");
            }
        }
        
        public async Task SaveDeviceTokenAsync(string userId, string deviceToken)
        {
            await _userRepository.SaveDeviceTokenAsync(userId, deviceToken);
        }
        
        public async Task<object> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }
        
        public async Task<UserRecord> VerifyIdTokenAsync(string idToken)
        {
            FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
            string uid = decodedToken.Uid;
            return await FirebaseAuth.DefaultInstance.GetUserAsync(uid);
        }
        
        private static string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
    
    
}
