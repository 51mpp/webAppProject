<<<<<<< HEAD
function showComment(id) {
=======
﻿function showComment(id) {
>>>>>>> a9a73f4fe22af47f5396286adc56b736ec198b22
    console.log("click");
    var main = document.getElementById(id);
    console.log(main.style.display);
    if (main.style.display === "block") {
        main.style.display = "none";
    } else {
        main.style.display = "block";
    }

}