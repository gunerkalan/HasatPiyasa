using System;
using System.Collections.Generic;
using System.Text;

namespace HasatPiyasa.Business.Constants
{
    public class Messages
    {
        public static string SusccesfulyLogin = "Sisteme Giriş Başarılı !";
        public static string ErrorDatabaseLogin = "Kullanıcı Veritabanında Kayıtlı Değil !";
        public static string ErrorDomainLogin = "Kullanıcı Şifreniz Hatalı, Lütfen Tekrar Deneyiniz !";
        public static string SuccessfulyAddSaleOrder = "Veriniz Başarıyla Oluşturuldu !";
        public static string ErrorAddSaleOrder = "Satış Siparişi Oluşturma Sırasında Bir Hata Oluştu ! ";
        public static string SuccessfulyAddSendOrder = "Numralı Sevk Siparişiniz, Başarıyla Oluşturuldu !";
        public static string ErrorAddSendOrder = "Sevk Siparişi Oluşturma Sırasında Bir Hata Oluştu ! ";
        public static string ErrorEmteaCode = "Girilen Emtea Kodu ile daha önce Emtea tanımlanmış ! ";
        public static string SuccessfulyAddEmtea = "Emtia Veritabanına Eklendi !";
        public static string DataInputControlError = " Bir şehire günde 1 kez kayıt yapılabilir ! ";
        public static string DataInputUpdate = " Güncelleme yapılmıştır ";
        public static string DataInputControlError = "Emtia tipiyle bir şehire günde 1 kez kayıt yapılabilir ! ";
        public static string ErrorEmteaGroupName = "Girilen Emtea Grup Name ile daha önce Emtea  Grup tanımlanmış ! ";
        public static string ErrorAdd = "Veritabanına kayıt edilemedi ! ";
    }
}
