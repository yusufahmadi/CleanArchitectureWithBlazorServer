-- Cara Pakai Code Generator
1 install CleanArchitecture CodeGenerator dari Extension
2 Cek Domain/Entity/Customer.cs Model -> inherit Auditable Entity
3 Ke Apllication/Features Klik kanan Add > New Application Feature > Pilih Entity Name


-- Contoh :
buat page Kendaraan
1 Buat Domain/Entity/Vehicle.cs inherit Auditable Entity
2 Sesuaikan Kolomnya & Buat Migration
3 Buka dan Tambahkan di 2 File Berikut :
    1 => Application/Common/Interface/IApplicationDBContext.cs  DbSet<Vehicle> Vehicles{ get; set; }
    2 => Infrastructure Presistence/ApplicationDBContext.cs     public DbSet<Vehicle> Vehicles { get; set; }
  
4 Lakukan Menggukana Kode Generator Step di Awal(1-3)
5 Buka File Server.UI/Services/Navigation/MenuServices.cs (untuk tambahkan di navbar)
6 Rebuild > Buka Web > Role Pastikan Access Right Sudah di set.

Note : Type Data Int ada Error di generate code bisa nyontek yang product.