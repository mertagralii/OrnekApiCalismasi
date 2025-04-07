
## 📦 DTO, AutoMapper, Include ve ICollection Nedir? Projeye Nasıl Dahil Edilir?

### ✅ DTO Nedir?
**DTO (Data Transfer Object)**, veri taşımak için kullanılan sınıftır. Genellikle sadece ihtiyaç duyulan alanları içerir.

### ✅ DTO Ne için Kullanılır?
- Kullanıcıya sadece gerekli veriyi göstermek için  
- Veritabanı modelini dış dünyadan saklamak için  
- Güvenlik ve performans amacıyla  
- Katmanlar arası veri taşımak için  

### ✅ AutoMapper Nedir?
**AutoMapper**, iki sınıf arasında otomatik dönüşüm (mapping) yapmamızı sağlar. Genelde Model ve DTO arasında kullanılır.

### ✅ AutoMapper Ne İşe Yarar?
- Manual mapping yerine otomatik dönüşüm sağlar  
- Kod tekrarını azaltır  
- Daha temiz ve okunabilir kod yazarız  

### ✅ AutoMapper Projeye Nasıl Dahil Edilir?

1. NuGet’ten AutoMapper paketini yüklersin:

   ```bash
   dotnet add package AutoMapper


2. Arından Program.cs'e aşağıdaki kodu eklersin : 
 ```c#
   builder.Services.AddAutoMapper(typeof(Program).Assembly); // AutoMapper Kullanacağımız Zaman bunu kullanmamız gerekiyor.
```

Arından Projenin içine bir MappingProfile.cs class'ı tanımlarsın.

 ```c#
  using AutoMapper;
using OrnekApiCalismasi.Models.Dtos.Classroom;
using OrnekApiCalismasi.Models.Dtos.Student;
using OrnekApiCalismasi.Models.Dtos.Teacher;

namespace OrnekApiCalismasi;

public class MappingProfile : Profile // AutoMapper NugetPack'i indirdikten sonra bu işlemi yapmamız gerekiyor.
{
    public MappingProfile()
    {
        CreateMap<Teacher, TeacherDto>(); // Teacher, TeacherDto'ya dönüşebilir. TeacherDto, Teacher'e dönüşebilir.
        CreateMap<Teacher, TeacherDetailDto>(); // Teacher, TeacherDetailDto'ya dönüşebilir. TeacherDetailDto Teacher'E dönüşebilir.
        CreateMap<TeacherCreateDto, Teacher>();
        CreateMap<Student, StudentDto>();
        CreateMap<Classroom, ClassroomDetailDto>();
        CreateMap<Classroom, ClassroomWithStudentsDto>();
    }
}
```

Ve artık AutoMapper'imiz de kurulmuş durumda.

### ✅ Bir DTO Ve Mapper Örneği : 

BBir proje açtığımızı ve gerekli paketleri kurduğumuzu varsayalım. CodeFirst yaklaşımını kullanarak veritabanımızı oluşturduk. Şimdi ise bu tablolarda bulunan bilgilerin hepsini kullanıcıya göstermek yerine, sadece belirli alanları göstermek istiyoruz. Bu noktada bir DTO kullanmak gereklidir.

Örnek Entities Modelimiz :
```c#
   namespace OrnekApiCalismasi;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Created { get; set; }

    public ICollection<Classroom> Classrooms { get; set; }
}
```

Bu modeli kullanıcıya gösterdiğimizde, kullanıcı tüm tablolara erişebilecektir. Ancak sadece Id ve Name değerlerini göstermek istiyoruz. Bunun için Models/Dtos/Student/StudentDto.cs dosyasını oluşturduk.

StudentDto.cs'in için : 
 
 ```c#
  namespace OrnekApiCalismasi.Models.Dtos.Student;

public class StudentDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}
```
Artık sadece gerekli verileri gönderebiliriz ve kullanıcı diğer tablodaki alanları göremez. Bu şekilde veri güvenliği sağlanmış olur.

✅ Öğrenci Ekleme Örneği:
Şimdi ise Student tablosuna bir öğrenci eklemek istiyoruz. Aşağıdaki örnek, DTO kullanarak nasıl ekleme yapılacağını göstermektedir:

```c#

 [HttpPost]
    public IActionResult AddTeacher([FromBody]TeacherCreateDto teacher) // Model olarak Dto kullandık.
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { Message = "Eksik veya hatalı giriş yaptınız." });
        }
        
        // var newTeacher = new Teacher {
        //     Name = teacher.Name,
        // };
        
        var newTeacher = _mapper.Map<Teacher>(teacher); // Dto'daki veriyi Teacher'e dönüştürdük.
        
        _context.Teachers.Add(newTeacher); // Eklemeyi yine tablolar üzerinden gerçekleştirdik.
        _context.SaveChanges();
        
        var result = _mapper.Map<TeacherDto>(newTeacher);
        
        // hangi id ile eklendiğini belirtmek ve oluşturulma tarihini göstermek istiyorsam newTeacher dönmeliyim
        // eğer sadece işlemin başarılı olduğunu söyleyeceksem. new { success = True } gibi bir sonuç dönebilirim.
        // return Ok(result);
        
        return CreatedAtAction(nameof(GetTeacher), new { id = result.Id }, result);
    }
```

Burada, kullanıcı sadece TeacherDto'yu görerek bilgilerini girecek ve ardından AutoMapper kullanarak bu veriyi Teacher modeline dönüştürüp veritabanına kaydedeceğiz. Son olarak, eklenen veriyi TeacherDto'ya dönüştürüp döndürüyoruz.


 



