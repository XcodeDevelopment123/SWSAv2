// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const urls = {
    "auth": "/Auth",
    "companies": "/companies",
    "company_owner":"/companies/owner",
    "company_official_contact": "/companies/official-contact",
    "company_communication_contact": "/companies/communication-contact",

};


function extractNumbers(value) {
    const match = value.match(/\d+/g); 
    return match ? match.join('') : null;
}