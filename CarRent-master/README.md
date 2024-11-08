# CarRent 
## Proje Genel Bakış
Bu, .NET Core kullanarak oluşturduğum ilk projedir ve bu teknolojiyle pratik yapmak amacıyla çevrimiçi bir mağaza oluşturulmuştur. Ayrıca üniversitedeki bir ders projesi olarak kullanılmış ve başarıyla değerlendirilmiştir.

**Web uygulaması için teknik görev, araba kiralama sürecini otomatikleştirmekti; daha spesifik olarak:**

Filo, farklı marka, fiyat ve türlerde birkaç araba içermektedir. Her arabanın kendine özgü bir kira fiyatı vardır. Müşteriler kiralama noktasına başvurur. Tüm müşteriler zorunlu bir kayıt sürecinden geçer, burada kendileri hakkında standart bilgiler (soyadı, adı, ikinci adı, adresi, telefon numarası) toplanır. Tüm müşteri talepleri kaydedilir ve her uzlaşma için veriliş ve beklenen iade tarihleri hatırlanır. Bir arabanın kira maliyeti, yalnızca arabanın kendisine değil, aynı zamanda kiralama süresine ve üretim yılına da bağlı olmalıdır. Arabanın uygunsuz durumda iadesi için bir ceza sistemi ve düzenli müşteriler için bir indirim sistemi de tanıtılması gerekmektedir.

## Kullanılan Teknolojiler:

- .NET Core MVC: Model-View-Controller (Model-Görünüm-Kontrolör) deseniyle web uygulamaları oluşturmak için.
  
- Entity Framework Core: Veritabanı işlemleri için ORM.

- Razor Pages: ASP.NET Core'da web kullanıcı arayüzleri oluşturmak için basitleştirilmiş bir programlama modeli.

- MSSQL: Veritabanı için Microsoft SQL Server.

## İşlevsellik:
Uygulama, araba kiralamalarını yönetmek için CRUD işlemleri, Identity aracılığıyla kullanıcı kimlik doğrulama ve yetkilendirme, mevcut arabaların görüntülenmesi, araba rezervasyon bilgileri ve kullanıcı profilleri gibi özellikleri içerir.
