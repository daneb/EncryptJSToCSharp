// replit.com - quick and easy test
var CryptoJS = require("crypto-js");

// Encrypt
var key = CryptoJS.enc.Utf8.parse("8056483646328763");
var iv = CryptoJS.enc.Utf8.parse("8056483646328763");
var utf8Message = CryptoJS.enc.Utf8.parse('my message');
var ciphertext = CryptoJS.AES.encrypt(utf8Message, key, {
  keySize: 128 / 8,
  iv: iv,
  mode: CryptoJS.mode.CBC,
  padding: CryptoJS.pad.Pkcs7
}).toString();
console.log(ciphertext);

var plainText = CryptoJS.AES.decrypt(ciphertext, key, {
  keySize: 128 / 8,
  iv: iv,
  mode: CryptoJS.mode.CBC,
  padding: CryptoJS.pad.Pkcs7
}).toString(CryptoJS.enc.Utf8);
console.log(plainText);
