export const addNewPost =(color)=>
{
    var mainC = document.getElementById("CC");
    var newdiv=document.createElement("div");
    newdiv.innerText="Kuuu";
    newdiv.style.cssText = 'height: 100px;';
    newdiv.style.backgroundColor = color;
    newdiv.className = "col-12 row"
    mainC.appendChild(newdiv);
    console.log("Kuy");
}

export const colorControl =()=>{
}
