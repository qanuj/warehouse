'use strict';
angular
  .module('humenize', [])
 .filter('uncamel', function () {
     function decamelize(str, sep) {
         if (typeof str !== 'string') {
             throw new TypeError('Expected a string');
         }
         return str.replace(/([a-z\d])([A-Z])/g, '$1' + (sep || '_') + '$2').toLowerCase();
     }
     return function (input, allLower) {

         if (typeof input !== "string") {
             return input;
         }

         var result = decamelize(input, ' ');

         if (!allLower) {
             result = result.charAt(0).toUpperCase() + result.slice(1);
         }

         return result;
     };
 })
 .filter('plaintext', function () {
     return function (html) {
         var div = document.createElement("div");
         div.innerHTML = html;
         return div.textContent || div.innerText || "";
     };
 });