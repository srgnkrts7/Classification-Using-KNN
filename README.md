# Classification Using KNN

Program 2 bölümünde olanları kısaca anlatmaya başlayacak olursak bizden tam olarak istenen şey doğada bulduğumuz herhangi bir çiçeğin hangi sınıfa ait olduğunu yazdığımız algoritmaya göre buldurmaktır. Bunun ayrıntısına girersek öncelikle elimizde 150 çiçeğe ait bir veri seti bulunmaktadır. Bu veri setinde bu çiçeklere ait 4 özellik (Taç Yaprak Uzunluğu-Taç Yaprak Genişliği-Çanak Yaprak Uzunluğu-Çanak Yaprak Genişliği) ve çiçeklerin ait olduğu sınıflar(Iris-setosa/Iris-versicolor/Iris-virginica) bulunmaktadır. Programda bizden istenen ise yukarıda da bahsettiğimiz gibi veri setinde olmayan bir çiçeğin özelliklerini alarak, bu özellikleri veri setindeki tüm değerlerle karşılaştırarak ve belirli uzaklık değerleri bularak en yakın uzaklığa sahip sınıfa yeni çiçeği eklemektir. Fakat en yakın uzaklıkları bulurken başka bir ayrıntı daha söz konusudur. Bu ayrıntı ise belirli sayıdaki en yakın uzaklıkları bulmaktır. Bunu daha da açacak olursak kullanıcıdan yeni çiçek özellikleri dışında bir de ‘K’ değeri alınacaktır. Örneğin kullanıcı k’yı 3 girerse en yakın (yani uzaklığı en küçük) 3 çiçek listelenecek ve bu 3 çiçek arasında hangi sınıftan daha fazla eleman varsa o sınıfa yerleştirilecektir. Eğer bu sınıflandırma bölümünde sınıflar arasında eşitlik olursa en yakın bitki türünde sınıflandırılacaktır. Bütün bu anlattıklarımız programın kNN algoritmasını oluşturacaktır.









Proje C# dilinde,Microsoft Visual Studio 2017 platformunda yazılmıştır.
