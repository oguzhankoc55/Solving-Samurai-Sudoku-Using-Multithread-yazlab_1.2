# yazlab_1.2

# MULTITHREAD KULLANARAK SAMURAI SUDOKU ÇÖZME

# Özet

Proje multithread yapısını kullanarak verilecek samurai sudoku üzerinden çözüm bulan bir masaüstü uygulamasıdır. Uygulama C# kullanılarak
geliştirilmiştir.Dokümanda projenin tanımı, çözüme yönelik yapılan araştırmalar, kullanılan fonksiyonlar, proje bitimindeki deneysel sonuçlar ve kod bilgisi gibi programın oluşumunu açıklayan başlıklara yer verilmiştir. Doküman sonunda projeyi hazırlarken kullandığımız kaynaklar bulunmaktadır.

# Giriş

Projede amaç multithread kullanılarak verilen samurai sudokuyu dinamik oloarak çözdürmektir.Sudoku başlangıç değerlerini .txt uzantılı dosyadan almaktadır.Dosyadan alınan bilgiler 9x9 luk 5 adet sudokuya gönderilir.Bu 9x9 luk sudokuların ortadaki sudokuyla geri kalan sudokular arasındaki sudokular arasındaki ortak alanlar çözdürülmeye çalışılınır.Çözümü form üzerinden gösterilir.Çıkan işlemlerin sonuçları .txt uzantılı dosyalara aktarılır.

# Problem Tanımı

Bu projede bizden multithread kullanılarak samurai sudoku çözdürememiz beklenmektedir. Bu sudokuda başlangıçta .txt uzantılı bir dosyadan okuma yapmamız
bekleniyor. Çözümlerden çıkan tüm işlemlerin .txt uzantılı dosyalra kaydedilir.Son olarak çıkan çözümleri kaç adet adımda yapıldığı gösterilir.

## Genel Yapı Fonksiyonlar
### karsilastir:
Bu fonksiyonda ortadaki sudoku ve köşelerdeki sudokuların ortak bölgelerindeki boş kutularda işlem yapılır.Sonra ihtimalleri_yaz_2 fonksiyonunda tutulan ihtimaller karşılaştırılıp yeni bir listede tutulur.
### karsilastir_new:
ihtimallerin tutulduğu liste de ikili kalanlaragöre doğru olan sonucu bulmaya çalışır.
### ihtimalleri_yaz_2:
Sudokuların ortak alanlarındaki bölegelerde boş olan noktaların olma ihtimallerini bulur ve listede tutar.
### tekrar:
karsilatir,ihtimlalleri_yaz_2 ve karsilastir_new fonksiyonlarını kullanarak ortak alanlarda bulunan noktaları çözer.
### Dosya_yukle:
Dosyadan veri alıp bu verileri ayrıştırarak sudokulara atılır.Böylece bu veriler diğer fonksiyonlar için hazır hale gelir.
### add_btun_Click:
Samuray sudokunun çözülmemiş hali gösterilir.
### sonuc_btn_Click:
Bu butonla threadler çalıştırılarak sudokunun çözümü dinamik olarak gösterilir
### islemler_kaydet:
Sudoku çözümü yaptırılırken yapılan adımların tümü bu fonksiyon sayesinde .txt uzantılı dosyalara göndermeye hazır hale getirilir
### txt_yaz:
islemler_kaydetde tutulan işlem adımları bu fonksiyon sayesinde dosyalara yazılır.
### solvesudoku:
Bu fonksiyon sayesinde backtracking yöntemi kullanılarak sudoku çözümü yapılır.
### uygunluk:
Gönderilen sayının gönderilen sudoku hücresine uygun olup olmadığına bakılır.

## Sınıflar
### durum:Bu sınıfta sudoku çözerken yapılan değişiklikler tutulur ihtimal: Bu sınıfta olması muhtemel ihtimaller tutulur.
### form2: Yapılan işlemleri grafik olarak gösteririz.

# Temel Bilgiler
Proje geliştirmede:
Programlama dili olarak “C#” kullanılmıştır.
Program geliştirme ortamı olarak “Visual Studio Code” kullanılmıştır.

# Yapılan Araştırmalar:
Başlangıçta Sudokuyu çözme konusunda zorlandık Sudokuyu çözebilmek için yöntemlar araştırma yoluna gittik.Bunun için en uygun yöntemin backtracking olduğunu düşündük.
https://www.geeksforgeeks.org/ Sitedeki çözümleri uygulamaya çalıştık.
Bundan sonra sudokuyu threade uygulamaya çalıştık.Bunun için thread hakkında araştırma yaptık.

# Deneysel Sonuçlar ve Ekran Görüntüleri
![image](https://user-images.githubusercontent.com/58952369/180196544-5e3f8f2a-e968-44ec-9ef1-0a8fd64b2cc9.png)

![image](https://user-images.githubusercontent.com/58952369/180196640-a53dfe51-90c4-49da-8eab-98fdcab02130.png)


# Kaynakça 
https://www.geeksforgeeks.org

https://docs.microsoft.com/tr-tr/dotnet/api/system.threading.thread?view=net-6.0 

https://stackoverflow.com

https://github.com 

https://tr.wikipedia.org/wiki/Sudoku

https://www.c-sharpcorner.com/article/c-sharp-write-to-file/
