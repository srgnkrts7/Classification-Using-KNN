using System;

namespace ClassificationKNN
{
    class Program
    {
        static void Main(string[] args)
        {
            int k;
            int secenek;
            double CanYapUz;
            double CanYapGen;
            double TacYapUz;
            double TacYapGen;
            // Dosyadan metin okuma ve okunan metni parçalara bölerek çift boyutlu listeye atma
            string[,] liste = new string[200, 5];
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Sergen\source\repos\Proje1B\Proje1B\Cicekler.txt");
            char[] ayrac = { ',' };
            int satir = 0;
            foreach (string line in lines)
            {
                int sutun = 0;
                string[] kelimeler = line.Split(ayrac);
                for (int i = 0; i < kelimeler.Length; i++)
                {
                    liste[satir, sutun] = kelimeler[i];
                    sutun++;
                }
                satir++;
            }
            //Kullanıcıya program kullanış kolaylığı sağlamak amacıyla oluşturulan menü bölümü
            while (true)
            {
                Console.WriteLine("######## MENU ########");
                for (int i = 0; i < 6; i++)
                {
                    if (i == 0)
                    {
                        Console.WriteLine("# 1.Eleman Ekleme(Çiçek özelliklerini girerken Nokta(.) yerine Virgül(,) giriniz)");

                    }
                    else if (i == 1)
                    {
                        Console.WriteLine("# 2.Eleman Silme");

                    }
                    else if (i == 2)
                    {
                        Console.WriteLine("# 3.Listeleme");
                    }
                    else if (i == 3)
                    {
                        Console.WriteLine("# 4.Tümünü Silme");
                    }
                    else if (i == 4)
                    {
                        Console.WriteLine("# 5.Başarı Ölçümü");
                    }
                    else if (i == 5)
                    {
                        Console.WriteLine("# 6.Çıkış");
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Lütfen bir seçenek giriniz(1-6): ");
                secenek = Convert.ToInt16(Console.ReadLine());
                if (secenek == 6)
                {
                    Console.WriteLine("##### Program Kullanıcı Tarafından Sonlandırılmıştır. ######");
                    break;
                }
                if (secenek > 6)
                {

                    Console.WriteLine("Yanlış seçenek girdiniz.Lütfen tekrar deneyiniz.");
                }
                //Kullanıcıdan alınan seçeğene göre gerekli metotları çağırıp işlemleri yapan bölüm
                switch (secenek)
                {
                    case 1:  //Kullanıcı yeni bir çiçek girdisi girerse bu bölüm çalışacaktır
                        Console.WriteLine("Lütfen k değerini giriniz");
                        k = Convert.ToInt16(Console.ReadLine());
                        Console.WriteLine("Lütfen Çiçeğin Çanak Yaprak Uzunluğunu Giriniz:");
                        CanYapUz = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Lütfen Çiçeğin Çanak Yaprak Genişliğini Giriniz:");
                        CanYapGen = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Lütfen Çiçeğin Taç Yaprak Uzunluğunu Giriniz:");
                        TacYapUz = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Lütfen Çiçeğin Çanak Yaprak Genişliğini Giriniz:");
                        TacYapGen = Convert.ToDouble(Console.ReadLine());
                        CicekYerlestir(k, CanYapUz, CanYapGen, TacYapUz, TacYapGen, liste);
                        Console.WriteLine();
                        break;
                    case 2:  //Kullanıcı belirli bir indisdeki elemanı silmek isterse bu bölüm çalışacaktır
                        Console.WriteLine("Lütfen silinecek verinin indisini giriniz:");
                        int indis = Convert.ToInt16(Console.ReadLine());
                        ElemanSil(liste, indis);
                        Console.WriteLine("Eleman Başarıyla Silinmiştir!");
                        Console.WriteLine();
                        break;
                    case 3: //Kullanıcı mevcut listeyi görüntülemek isterse bu bölüm çalışacaktır
                        Console.WriteLine("######## MEVCUT LİSTE ########\n");
                        Listele(liste);
                        Console.WriteLine();
                        break;
                    case 4:  //Kullanıcı listedeki tüm elemanları silmek isterse bu bölüm çalışacaktır
                        TumunuSil(liste);
                        Console.WriteLine("Listedeki Tüm Elemanlar Silinmiştir!");
                        Console.WriteLine();
                        break;
                    case 5:  //Kullanıcı başarı ölçümü yapmak isterse bu bölüm çalışacaktır
                        BasarıOlcum(liste);
                        Console.WriteLine();
                        break;
                    default:
                        break;
                }
            }
            Console.ReadKey();
        }
        //Bir çiçeğe ait 4 adet özelliği ve belli bir k değerini alıp listeye ekleyen metot(kNN)
        static void CicekYerlestir(int k, double CanYapUz, double CanYapGen, double TacYapUz, double TacYapGen, string[,] liste)
        {
            string[] tur = new string[k];  //K sayısına göre en yakın uzaklıktaki tür isimlerini tutan liste
            double[] dUzaklık = new double[200];  //Girilen çiçeği tüm çiçeklerle kıyaslayıp tüm çiçekler ile olan uzaklarını tutan liste
            double[] kDeger = new double[k]; //Girilen k sayısındaki en kısa uzaklıkları tutan liste
            string[] kDegerstr = new string[k]; //kDeger listesinin string hali
            string[,] kliste = new string[k, 5]; // Girilen k sayısındaki en kısa uzaklıklara sahip çiçeklerin özelliklerini tutan liste
            double d = 0;
            double ceviri;
            int cSetosa = 0; //Tür sayaçları
            int cVersicolor = 0; //Tür sayaçları
            int cVirginica = 0; //Tür sayaçları
            string tur1 = null;
            //Gerekli hesaplamaları yapıp girilen çiçeği verisetindeki tüm çiçeklere karşılaştırıp uzaklıklarını dUzaklık'a kaydeden bölüm
            for (int i = 0; i < liste.GetLength(0); i++)
            {
                d = 0;
                for (int j = 0; j < liste.GetLength(1); j++)
                {
                    if (liste[i, j] == null)
                    {
                        break;
                    }
                    if (j == 4)
                    {
                        continue;
                    }
                    else if (j == 0 && liste[i, j] != null)
                    {
                        string a = liste[i, j].Replace(".", ",");
                        ceviri = Convert.ToDouble(a);
                        d += Math.Sqrt(Math.Pow(CanYapUz - ceviri, 2));
                    }
                    else if (j == 1 && liste[i, j] != null)
                    {
                        string a = liste[i, j].Replace(".", ",");
                        ceviri = Convert.ToDouble(a);
                        d += Math.Sqrt(Math.Pow(CanYapGen - ceviri, 2));
                    }
                    else if (j == 2 && liste[i, j] != null)
                    {
                        string a = liste[i, j].Replace(".", ",");
                        ceviri = Convert.ToDouble(a);
                        d += Math.Sqrt(Math.Pow(TacYapUz - ceviri, 2));
                    }
                    else if (j == 3 && liste[i, j] != null)
                    {
                        string a = liste[i, j].Replace(".", ",");
                        ceviri = Convert.ToDouble(a);
                        d += Math.Sqrt(Math.Pow(TacYapGen - ceviri, 2));
                    }
                }
                dUzaklık[i] = d;
            }
            //dUzaklık listesinin tüm elemanlarını gezip minimum uzaklığa sahip k adet bitkinin uzaklıklarını kDeger'e, özelliklerini kListe'ye kaydeden bölüm
            for (int n = 0; n < k; n++)
            {
                double minDeger = 500;
                for (int j = 0; j < dUzaklık.Length; j++)
                {
                    if (dUzaklık[j] == 0)
                    {
                        break;
                    }
                    if (kDeger[0] != 0)
                    {
                        if (dUzaklık[j] < minDeger)
                        {
                            if (kDeger[n - 1] < dUzaklık[j] && kDeger[n] == 0)
                            {
                                minDeger = dUzaklık[j];
                                tur1 = liste[j, 4];
                                kliste[n, 0] = liste[j, 0];
                                kliste[n, 1] = liste[j, 1];
                                kliste[n, 2] = liste[j, 2];
                                kliste[n, 3] = liste[j, 3];
                                kliste[n, 4] = liste[j, 4];
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (dUzaklık[j] < minDeger)
                        {
                            minDeger = dUzaklık[j];
                            tur1 = liste[j, 4];
                            kliste[n, 0] = liste[j, 0];
                            kliste[n, 1] = liste[j, 1];
                            kliste[n, 2] = liste[j, 2];
                            kliste[n, 3] = liste[j, 3];
                            kliste[n, 4] = liste[j, 4];
                        }
                    }
                }
                kDeger[n] = minDeger;
                string x = String.Format("  {0:F3}", minDeger); //Virgülden sonra 3 basamaklı yazdırma
                kDegerstr[n] = x;
                // En yakın türün hangisi olduğunu bulup o türe ait sayacı arttırıp tür ismini tur listesine kaydeden bölüm
                if (tur1.Equals("Iris-setosa"))
                {
                    cSetosa++;
                }
                else if (tur1.Equals("Iris-versicolor"))
                {
                    cVersicolor++;
                }
                else
                {
                    cVirginica++;
                }
                tur[n] = tur1;
            }
            string sinif;
            //En yakın bitkilerin sınıflarına bakıp hangi sınıf sayısından daha çok eleman var ise o sınıfa ekleyen bölüm
            if ((cSetosa > cVersicolor && cSetosa > cVirginica))
            {
                Console.WriteLine();
                Console.WriteLine("Yerleştirilmesi gereken sınıf: " + "Iris-setosa");
                sinif = "Iris-setosa";
                ElemanEkle(CanYapUz, CanYapGen, TacYapUz, TacYapGen, sinif, liste);

            }
            else if (cVirginica > cSetosa && cVirginica > cVersicolor)
            {
                Console.WriteLine();
                Console.WriteLine("Yerleştirilmesi gereken sınıf: " + "Iris-virginica");
                sinif = "Iris-virginica";
                ElemanEkle(CanYapUz, CanYapGen, TacYapUz, TacYapGen, sinif, liste);
            }
            else if (cVersicolor > cSetosa && cVersicolor > cVirginica)
            {
                Console.WriteLine();
                Console.WriteLine("Yerleştirilmesi gereken sınıf: " + "Iris-versicolor");
                sinif = "Iris-versicolor";
                ElemanEkle(CanYapUz, CanYapGen, TacYapUz, TacYapGen, sinif, liste);
            }
            //Eşitlik durumlarında en yakın bitkinin sınıfında sınıflandıran bölümler
            else if (cSetosa < cVirginica && cVirginica == cVersicolor)
            {
                for (int i = 0; i < tur.Length; i++)
                {
                    if (tur[i].Equals(cVirginica) || (tur[i].Equals(cVersicolor)))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Yerleştirilmesi gereken sınıf: " + tur[i]);
                        sinif = tur[i];
                        ElemanEkle(CanYapUz, CanYapGen, TacYapUz, TacYapGen, sinif, liste);
                        break;
                    }
                }
            }
            else if (cVirginica < cVersicolor && cSetosa == cVersicolor)
            {
                for (int i = 0; i < tur.Length; i++)
                {
                    if (tur[i].Equals(cSetosa) || (tur[i].Equals(cVersicolor)))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Yerleştirilmesi gereken sınıf: " + tur[i]);
                        sinif = tur[i];
                        ElemanEkle(CanYapUz, CanYapGen, TacYapUz, TacYapGen, sinif, liste);
                        break;
                    }
                }
            }
            else if (cVersicolor < cSetosa && cVirginica == cSetosa)
            {
                for (int i = 0; i < tur.Length; i++)
                {
                    if (tur[i].Equals(cSetosa) || (tur[i].Equals(cVirginica)))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Yerleştirilmesi gereken sınıf: " + tur[i]);
                        sinif = tur[i];
                        ElemanEkle(CanYapUz, CanYapGen, TacYapUz, TacYapGen, sinif, liste);
                        break;
                    }
                }
            }
            else if (cVirginica == cVersicolor && cVersicolor == cSetosa)
            {
                Console.WriteLine();
                Console.WriteLine("Yerleştirilmesi gereken sınıf: " + tur[0]);
                sinif = tur[0];
                ElemanEkle(CanYapUz, CanYapGen, TacYapUz, TacYapGen, sinif, liste);
            }
            // Girilen çiçek özelliklerine göre bulunan en yakın çiçeklerin özelliklerinin ve uzaklıklarının tablosunu yazdıran bölüm
            Console.WriteLine("Girilen çiçek özelliklerine göre en yakın çiçekler ve özellikleri tablosu");
            int sayac = 0;
            for (int i = 0; i < kliste.GetLength(0); i++)
            {
                for (int j = 0; j < kliste.GetLength(1); j++)
                {
                    if (kliste[i, j].Length == 1)
                    {
                        Console.Write(kliste[i, j] + "   ");
                    }
                    else
                    {
                        Console.Write(kliste[i, j] + " ");
                    }
                }
                if (kliste[i, 4] == "Iris-setosa")
                {
                    Console.Write("       Uzaklık: " + kDegerstr[sayac]);
                }
                else if (kliste[i, 4] == "Iris-virginica")
                {
                    Console.Write("    Uzaklık: " + kDegerstr[sayac]);
                }
                else
                {
                    Console.Write("   Uzaklık: " + kDegerstr[sayac]);
                }
                sayac++;
                Console.WriteLine();
            }
        }
        //Test verisinden her sınıfa ait son 10 çiçeği ayırıp bu çiçekleri geri kalan 120 çiçeğe CicekYerlestir metodu ile yerleştiren bölüm
        static void BasarıOlcum(string[,] liste)
        {
            string[,] test = new string[30, 5];
            int k = 0;
            int m;
            double setosaSayac = 0;
            double versicolorSayac = 0;
            double virginicaSayac = 0;
            //30luk test verisini ayırma bölümü
            for (int i = 40; i <= 49; i++)
            {
                m = 0;
                for (int j = 0; j < 5; j++)
                {
                    test[k, m] = liste[i, j];
                    m++;
                    liste[i, j] = null;
                }
                k++;
            }
            for (int i = 90; i <= 99; i++)
            {
                m = 0;
                for (int j = 0; j < 5; j++)
                {
                    test[k, m] = liste[i, j];
                    m++;
                    liste[i, j] = null;
                }
                k++;
            }
            for (int i = 140; i <= 149; i++)
            {
                m = 0;
                for (int j = 0; j < 5; j++)
                {
                    test[k, m] = liste[i, j];
                    m++;
                    liste[i, j] = null;
                }
                k++;
            }
            //30 adet veri için bir adet k değeri alınan bölüm
            Console.WriteLine("Lütfen k değerini giriniz: ");
            int knn = Convert.ToInt16(Console.ReadLine());
            //Test listesindeki elemanları CicekYerlestir metodu ile sınıflandıran bölüm
            for (int i = 0; i < test.GetLength(0); i++)
            {
                string a = test[i, 0].Replace(".", ",");
                double test1 = Convert.ToDouble(a);
                string b = test[i, 1].Replace(".", ",");
                double test2 = Convert.ToDouble(b);
                string c = test[i, 2].Replace(".", ",");
                double test3 = Convert.ToDouble(c);
                string d = test[i, 3].Replace(".", ",");
                double test4 = Convert.ToDouble(d);
                CicekYerlestir(knn, test1, test2, test3, test4, liste);
                //Başarı oranını bulmak için doğru yerleştirilen elemanların sayısını bulan bölüm
                if (liste[0, 4] == test[i, 4] && i < 10)
                {
                    setosaSayac++;
                }
                else if (liste[50, 4] == test[i, 4] && i < 20 && i > 10)
                {
                    virginicaSayac++;
                }
                else if (i >= 20)
                {
                    versicolorSayac++;
                }
            }
            double toplam = versicolorSayac + virginicaSayac + setosaSayac;
            Console.WriteLine();
            Console.WriteLine("Başarı Oranı: " + toplam / 30);
            Console.WriteLine("Başarı Oranı(Yüzde Olarak): " + "%" + 100 * (toplam / 30));
        }
        //Özelliklerini parametre olarak aldığı çiçeği verisetine ekleyen metot
        static void ElemanEkle(double CanYapUz, double CanYapGen, double TacYapUz, double TacYapGen, string sinif, string[,] liste)
        {
            for (int i = 0; i < liste.GetLength(0); i++)
            {
                if (liste[i, 0] == null)
                {
                    liste[i, 0] = Convert.ToString(CanYapUz);
                    liste[i, 1] = Convert.ToString(CanYapGen);
                    liste[i, 2] = Convert.ToString(TacYapUz);
                    liste[i, 3] = Convert.ToString(TacYapGen);
                    liste[i, 4] = sinif;
                    break;
                }
                else
                    continue;
            }
        }
        //Verilen bir indisteki elemanı silen metot
        static void ElemanSil(string[,] liste, int indis)
        {
            for (int j = 0; j < liste.GetLength(1); j++)
            {
                liste[indis, j] = null;
            }
        }
        //Tüm verisetindeki verileri temizleyen metot
        static void TumunuSil(string[,] liste)
        {
            for (int i = 0; i < liste.GetLength(0); i++)
            {
                for (int j = 0; j < liste.GetLength(1); j++)
                {
                    liste[i, j] = null;
                }
            }
        }
        //Tüm verisetindeki bilgileri ekrana yazdıran metot
        static void Listele(string[,] liste)
        {
            for (int i = 0; i < liste.GetLength(0); i++)
            {
                for (int j = 0; j < liste.GetLength(1); j++)
                {
                    if (liste[i, j] == null)
                        continue;
                    if (liste[i, j].Length == 1)
                    {
                        Console.Write(liste[i, j] + "   ");
                    }
                    else
                    {
                        Console.Write(liste[i, j] + " ");
                    }

                }
                if (liste[i, 0] != null)
                {
                    Console.WriteLine();
                }
            }
        }
    }
}                                                                                                                   //Sergen KARATAŞ 05150000637
                                                                                                                    //Mert Gülbahçe  05150000684
