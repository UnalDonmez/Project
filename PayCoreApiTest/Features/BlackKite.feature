Feature: BLACKkİTE
	Simple calculator for adding two numbers

@mytag
Scenario: BlackKite Success Login
* Driver'i ayaga kaldir
* https://seam.riskscore.cards/ adresine git
* 1 saniye bekle
* LoginButton elementinin görünürlüğü kontrol edilir
* EmailInput elementine alper.keles@testinium.com textini yaz
* PasswordInput elementine n!Ak483;$Fr91 textini yaz
* KeepMeSignInButton elementine tıkla
* 2 saniye bekle
* LoginButton elementine tıkla
* sayfa Dashboard değerini içeriyor mu
* 2 saniye bekle
* 2 saniye bekle
* 2 saniye bekle


Scenario: BlacKite UnSuccess Login
* Driver'i ayaga kaldir
* https://seam.riskscore.cards/ adresine git
* 1 saniye bekle
* LoginButton elementinin görünürlüğü kontrol edilir
* EmailInput elementine YanlisMail textini yaz
* PasswordInput elementine YanlisSifre textini yaz
* LoginButton elementine tıkla
* 3 saniye bekle
* sayfa The Email field is not a valid e-mail address. değerini içeriyor mu
* 3 saniye bekle



