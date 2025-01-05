using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//3.Hangi Yaklaşımı Seçmeli?

//A. Küçük ve Orta Ölçekli Projeler:
//Service Layer İçinde Mapper Kullanımı daha uygun olabilir.
//Daha az karmaşıklık ve hızlı geliştirme süreci sağlar.
//Yönetimi ve bakımı daha kolaydır.

//B. Büyük ve Karmaşık Projeler:
//CQRS İçinde Mapper Kullanımı tercih edilebilir.
//Ayrık sorumluluklar, projeyi daha yönetilebilir kılar.
//Performans optimizasyonları ve ölçeklenebilirlik sağlar.
//Kod tabanını daha temiz ve modüler hale getirir.

// yani mapper cqrs içinde gerçekleşmeli sorumluluk prensibine göre...