window.addEventListener("load", (event) => {
    console.log(window.location.pathname)
    var renderContainer = document.getElementById("renderContainer");
    var navTop = document.getElementById("mainnav");
    if (window.location.pathname == '/') {
        console.log("Home");
        renderContainer.className = "";
        document.getElementById("l01").style.color = "white"
        document.getElementById("li01").style.backgroundColor = "red"
    }
    else if (window.location.pathname == '/MainPose') {
        console.log("mainPost")
        document.getElementById("l02").style.color = "white"
        document.getElementById("li02").style.backgroundColor = "red"
        document.getElementById("footer").style.display = "none"
        renderContainer.className = "container";

    }
    else if (window.location.pathname == '/MainPose/CreateMainPose') {
        console.log("mainPost")
        document.getElementById("l02").style.color = "white"
        document.getElementById("li02").style.backgroundColor = "red"
        document.getElementById("footer").style.display = "none"
    }
    else if (window.location.pathname == '/Account/Login' || window.location.pathname == '/Account/Register') {
        console.log("Login")
        document.getElementById("li04").style.color = "white"
        document.getElementById("li04").style.backgroundColor = "red"
        document.getElementById("footer").style.display = "none"
        renderContainer.className = "";

    }
    else if (window.location.pathname == '/Deposit') {
        console.log("Login")
        document.getElementById("li03").style.color = "white"
        document.getElementById("li03").style.backgroundColor = "red"
        document.getElementById("footer").style.display = "none"

    }
    else if (window.location.pathname == '/Deposit/CreateDeposit') {
        console.log("Login")
        document.getElementById("li03").style.color = "white"
        document.getElementById("li03").style.backgroundColor = "red"
        document.getElementById("footer").style.display = "none"

    }
    else if (window.location.pathname == '/Deposit/EditMainPose/*/') {
        console.log("Login")
        document.getElementById("li03").style.color = "white"
        document.getElementById("li03").style.backgroundColor = "red"
        document.getElementById("footer").style.display = "none"

    }
    else if (window.location.pathname == '/Dashboard') {
        console.log("Login")
        document.getElementById("li05").style.color = "white"
        document.getElementById("li05").style.backgroundColor = "red"
        document.getElementById("footer").style.display = "none"
        renderContainer.className = "";
    }
    else if (window.location.pathname == '/Dashboard/EditUserProfile') {
        console.log("waht");
        document.getElementById("li05").style.color = "white"
        document.getElementById("li05").style.backgroundColor = "red"
        document.getElementById("footer").style.display = "none"
        renderContainer.className = "";
    }
});