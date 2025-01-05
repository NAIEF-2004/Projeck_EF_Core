using Class_Domain;
using Class_Data;
using Microsoft.EntityFrameworkCore;

namespace Projeck_EF_Core
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new PharmacyDbContext())
            {
                context.Database.EnsureCreated();

               
                var pharmacist = new Pharmacist//اضافة صيدلية مع المعلومات الخاصة بها 
                {
                    FirstName = "Naief",
                    LastName =  "Hamide",
                    ContactInfo = new ContactInfo
                    {
                        Email = "naief.hamide@example.com",
                        Phone = "123456789",
                        Address = "Azaz, Syria"
                    }
                };
                context.Pharmacists.Add(pharmacist);
                context.SaveChanges();
                Console.WriteLine("\t\t\t\t\t\t\t\t***Pharmacist yes add successfully***\n\n\n");

                
                var medicine = new Medicine//  اضافة دواء مع المعلومات الخاصة  
                {
                    Name = "Acetaminophen",
                    Manufacturer = "Tasnem",
                    Stock = 500,
                    Price = 7.5m
                };
                context.Medicines.Add(medicine);
                context.SaveChanges();
                Console.WriteLine("_Medicine added successfully.");

                // إضافة وصفة طبية 
                var prescription = new Prescription
                {
                    PatientName = "ali",
                    Quantity = 5,
                    IssuedDate = DateTime.Now,
                    PharmacistId = pharmacist.Id,
                    Medicines = new List<Medicine> { medicine }
                };
                context.Prescriptions.Add(prescription);
                context.SaveChanges();
                Console.WriteLine("_Prescription added successfully.");

                // من هنا يتم التحديث بعد ان تصدر الوصفة 
                var existingMedicine = context.Medicines.FirstOrDefault(m => m.Name == "Paracetamol");
                if (existingMedicine != null && existingMedicine.Stock >= prescription.Quantity)
                {
                    existingMedicine.Stock -= prescription.Quantity;
                    context.SaveChanges();
                    Console.WriteLine("_Stock updated successfully");
                }

                //وضع الاسم الاول للصيدلية في الاستعلام لكي يتم حذفها من هنا
                var pharmacistToDelete = context.Pharmacists.FirstOrDefault(p => p.FirstName == "");
                if (pharmacistToDelete != null)
                {
                    pharmacistToDelete.IsDeleted = true;
                    context.SaveChanges();
                    Console.WriteLine("__Pharmacist soft-deleted successfully__");
                }

                // استرجاع كل البيانات
                var pharmacists = context.Pharmacists
                    .Include(p => p.ContactInfo)
                    .Include(p => p.Prescriptions)
                    .Where(p => !p.IsDeleted)
                    .ToList();
                Console.WriteLine("\t\t\t\t\t\t\t\t         ***Show information***\n\n\n");

                Console.WriteLine("_[Pharmacists]_ :\n");//اوامر طباعة للمعلومات الخاصة بالمشروع
                foreach (var pharm in pharmacists)
                {
                    Console.WriteLine($"Name: {pharm.FirstName} {pharm.LastName},\n Email: {pharm.ContactInfo.Email}\n\n");
                    Console.WriteLine("_[Prescription]_ :");
                    foreach (var pres in pharm.Prescriptions)
                    {
                        Console.WriteLine($"  Prescription for: {pres.PatientName},\n Issued: {pres.IssuedDate}\n\n");
                    }
                }

                var medicines = context.Medicines.ToList();
                Console.WriteLine("_[Medicines]_ :");
                foreach (var med in medicines)
                {
                    Console.WriteLine($"Name: {med.Name}\n, Stock: {med.Stock}\n, Price: {med.Price}");
                }

            
            }
        }
    }
}

