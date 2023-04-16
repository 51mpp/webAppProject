function showComment(id) {
    console.log("click");
    var main = document.getElementById(id);
    console.log(main.style.display);
    if (main.style.display === "block") {
        main.style.display = "none";
    } else {
        main.style.display = "block";
    }

}