
# TPS Game - Köyünü Koru

Bu projenin, Unity kullanılarak yapılan bir Third Person Shooter oyunudur. Oyuncu klavye ve mouse ile karakteri yönetir. 
>Senaryo: Bir köyü zombi salgını esir almıştır. Köyünü bu durumdan kurtarmak isteyen köylü aksiyon almaya başlar.


### Oyuncu Mekanikleri

| Tuş / Girdi | Mekanik | Açıklama |
|--------------|----------|----------|
| **Shift** | Koşma (Sprint) | Oyuncu hızlı hareket eder |
| **C** | Çömelme (Crouch) | Oyuncu alçak duruşa geçer |
| **Sağ Tık (Mouse)** | Nişan Alma (Aim) | Kamera omuz hizasına geçer |
| **Sol Tık (Mouse)** | Ateş Etme (Fire) | Silah mermi fırlatır veya animasyon oynatır |
| **Sol Alt (Left Alt)** | Omuz Değiştirme (Shoulder Swap) | Kamera sol/sağ omuza geçer |

### Düşman Mekanikleri

| Durum | Tetikleyici (Ne zaman geçer?) | Davranış (Ne yapar?) |
|--------|-------------------------------|-----------------------|
| **Patrol (Devriye)** | Oyuncu tespit mesafesi dışında | Düşman iki nokta arasında ileri geri yürür |
| **Chase (Takip)** | Oyuncu tespit mesafesine girer | Oyuncuya doğru koşar |
| **Attack (Saldırı)** | Oyuncu çok yakına gelir | Vurma animasyonu oynatır, hasar verir |
| **Return (Geri Dön)** *(opsiyonel)* | Oyuncu görüş alanından çıkarsa | Devriye noktasına geri döner |

### Tasarım ve Sahne Yapısı

Oyun **3 ana sahneden** oluşmaktadır:

###  Main Menu Scene
- Oyun başlatma ve çıkış seçeneklerini içerir.
- resim

###  Game Scene 
- Oyunun ana oynanış kısmıdır.  
- Oyuncu mekanikleri (koşma, nişan alma, ateş etme vb.) bu sahnede aktif hâle gelir.
- resim

### Pause Scene
- Esc tuşuna basılarak erişilir.
- Baştan başlatma, ana menüye dönme seçeneklerini içerir.

### Game Over Scene
- Oyuncu öldüğünde veya tüm düşmanları öldürdüğünde görüntülenir.  
- resim

### Örnek Çalışmalar ve Karşılaştırmalar

| **Kaynak** | **Çalışma** | **Benzerlik / Farklılık** |
|-------------|--------------|-----------------------------|
| **Unreal Engine Shooter Template** | Unreal Engine üzerinde geliştirilmiş, temel TPS kontrol sistemi sunan şablondur. Karakter hareketi ve kamera kontrolleri bulunur. | Kamera ve kontrol yapısı benzerlik gösterir; ancak bu proje Unity ve C# ile geliştirilmiştir. FSM kullanımıyla davranış yönetimi sağlanmış, bu yönüyle Unreal şablonundan ayrılmaktadır. |
| **Unity Third Person Shooter Tutorial (Brackeys)** | Unity de yapılmış TPS projesidir. Kamera takibi, hedef alma ve ateş mekaniklerini içerir. | Karakter kontrol sistemi benzer yapıdadır, ancak Brackeys’in örneğinde düşman yapay zekâsı basittir. Bu projede FSM yapısıyla zombi davranışları yapılmıştır. |
| **Zombi AI (Blackthornprod)** | Unity’de zombi düşman yapay zekâsı üzerine hazırlanmış bir örnek. Zombiler oyuncuyu görme mesafesi içinde kovalayıp saldırır. | Benzer düşman tipi kullanılmıştır; ancak bu projede görüş açısı bulunmaz, mesafeye göre düşman harekete geçer.|
| **Mixamo TPS Prototype** | Mixamo karakterleriyle oluşturulmuş bir TPS projesidir. | Benzer şekilde Mixamo animasyonları kullanılmıştır, ancak bu projede sadece oyuncu karakterinde Blend Tree ile animasyon geçişleri uygulanmıştır. |
| **Gears of War** | AAA düzeyinde, cover alma ve taktiksel nişan sistemleriyle TPS türünün önemli bir örneğidir. | Bu proje ölçek olarak çok daha basittir; ancak Gears of War’daki düşman takibi fikrinden esinlenilmiştir. Zombilerin oyuncuyu belirli bir mesafede kovalamaya başlaması bu fikirden sadeleştirilmiş biçimde uyarlanmıştır. |

### Yazılımsal Mimari

Proje geliştirilirken, oyun içi sistemlerin modüler, genişletilebilir ve yönetilebilir olmasına dikkat edilmiştir. Bu amaçla aşağıdaki yazılımsal mimari ve teknik yaklaşımlar kullanılmıştır:

#### 1. **Oyun Mimarisi: Bileşen Tabanlı (Component-Based Architecture)**
- Unity’nin temel mimarisi olan **Component-Based Architecture** benimsenmiştir.  
- Her game object kendi bileşenleriyle kontrol edilmiştir.  


####  2. **Düşman Yapay Zekâsı: Finite State Machine + NavMesh AI**
- Düşman davranışları **Finite State Machine (FSM)** ile yapılmıştır.  
- Düşman üç ana durumda hareket eder: **Patrol, Chase, Attack**.  
- **Unity NavMesh Agent** sistemi kullanılarak düşmanlara **otomatik rota oluşturma ve engel tespiti** özelliği kazandırılmıştır.  

####  3. **Oyuncu Kontrol Sistemi**
- Oyuncunun hareketleri, **Input System** kullanılarak yönetilmiştir.  
- Tuş kombinasyonları (Shift ile koşma, C ile çömelme, Sağ Tık ile nişan alma, Sol Tık ile ateş etme, Sol Alt ile omuz değiştirme) event tabanlı şekilde kontrol edilmiştir.  

####  4. **TPS Kamera Sistemi**
- **Cinemachine** kullanılarak üçüncü şahıs kamera sistemi tasarlanmıştır.  
- Kamera, oyuncunun hareketine göre eşlik eder.  
- **Shoulder Swap (Omuz Değişimi)** mekaniği eklenmiş ve kamera pozisyonu runtime sırasında dinamik olarak değiştirilmiştir.



### Karşılaşılan Zorluklar ve Getirilen Çözümler

#### 1. **Uygun Asset Bulma Zorluğu**
- **Sorun:** Oyunun görsel yapısı için kullanılacak **low-poly** model ve çevre asset’lerini bulmakta zorlanıldı.  
  Bütçe kısıtları nedeniyle ücretli asset’ler tercih edilemedi. Dolayısıyla uygun temaya ait asset bulmakta zorlanıldı.  
- **Çözüm:**  
  - **Unity Asset Store** üzerindeki ücretsiz içerikler araştırıldı.  
  - Birbirinden farklı temaya sahip olan assetler bir araya getirilip ortak bir temaya dönüştürüldü.  

---

#### 2. **Oyuncunun Silah Tutuşu Problemi**
- **Sorun:** Oyuncu karakterin silah tutuş animasyonu ve el hizası tam olarak istenen şekilde ayarlanamadı.  
  Bu durum, nişan alma pozisyonunun biraz hatalı görünmesine neden oldu.  
- **Çözüm:**  
  - Geçici çözüm olarak el pozisyonu manuel biçimde ayarlandı.  

---

#### 3. **Düşmanın Oyuncuyu Takip Etme (Chase) Problemi**
- **Sorun:** Düşmanların oyuncuyu düzgün bir şekilde takip etmesi ve engelleri aşması beklenen gibi çalışmadı.  
  İlk denemelerde düşmanlar nesnelere takılıyor veya rastgele yönlerde hareket ediyordu.  
- **Çözüm:**  
  - Unity’nin **NavMesh Agent** sistemi kullanıldı.    
  - NavMesh, harita sınırları içinde yeniden oluşturularak düşmanların doğal hareket etmesi sağlandı.  

---
## Yapanlar

- [@CanerÇakar ](https://www.github.com/caner-cakar)
- [@FurkanUğurlu ](https://www.github.com/furkanugurlu)
- [@MelisaCeylan ](https://www.github.com/MelisaCeylan22)

  