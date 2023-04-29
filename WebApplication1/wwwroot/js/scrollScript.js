const mainnav = document.querySelector('nav')
var navitem = document.querySelectorAll('.nav-item')

window.onscroll = function () {
    if (window.scrollY > 80) {

        mainnav.classList.add('active');
        for (var i = 0; i < navitem.length; i++) {
            navitem[i].classList.add('active');
        }

        if (window.location.pathname == "/") {
            document.getElementById("l01").style.color = "white"
            document.getElementById("li01").style.backgroundColor = "#f59317"
            
        }
        else if (window.location.pathname == "/MainPose") {
            document.getElementById("l02").style.color = "white"
            document.getElementById("li02").style.backgroundColor = "#f59317"
        }
        else if (window.location.pathname == '/Deposit') {
            document.getElementById("l03").style.color = "white"
            document.getElementById("li03").style.backgroundColor = "#f59317"
        }
        else if (window.location.pathname == "/Account/Login" || window.location.pathname == '/Account/Register') {
            document.getElementById("l04").style.color = "white"
            document.getElementById("li04").style.backgroundColor = "#f59317"
        }
        else if (window.location.pathname == '/Dashboard') {
            document.getElementById("l05").style.color = "white"
            document.getElementById("li05").style.backgroundColor = "#f59317"
        }
    }
    else {
        mainnav.classList.remove('active');
        for (var i = 0; i < navitem.length; i++) {
            navitem[i].classList.remove('active');
        }
        if (window.location.pathname == "/") {
            document.getElementById("l01").style.color = "white"
            document.getElementById("li01").style.backgroundColor = "red"
            document.getElementById("bg-img").style.filter = "brightness(1)"
        }
        else if (window.location.pathname == "/MainPose") {
            document.getElementById("l02").style.color = "white"
            document.getElementById("li02").style.backgroundColor = "red"
        }
        else if (window.location.pathname == "/Deposit") {
            document.getElementById("l03").style.color = "white"
            document.getElementById("li03").style.backgroundColor = "red"
        }
        else if (window.location.pathname == "/Account/Login" || window.location.pathname == '/Account/Register') {
            document.getElementById("l04").style.color = "white"
            document.getElementById("li04").style.backgroundColor = "red"
        }
        else if (window.location.pathname == '/Dashboard') {
            document.getElementById("l05").style.color = "white"
            document.getElementById("li05").style.backgroundColor = "red"
        }

    }

}
    