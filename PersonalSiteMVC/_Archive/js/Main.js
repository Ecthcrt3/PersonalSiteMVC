window.onload = (event) => {
    let pageLoad = setTimeout(function(){
        let loader = document.getElementById("loader");
        loader.style.display = 'none'; 
    }, 1000);
}


localStorage.setItem('menuOpen', false);

ToggleNav = () => {
    //creating variables for html items
    let navToggler = document.getElementById("navToggler");
    let navList = document.getElementById("navList");

    //reversing the status of the menuOpen boolean and storing it in local storage
    let navOpen = JSON.parse(localStorage.getItem('menuOpen'));
    navOpen = !navOpen;
    localStorage.setItem('menuOpen', navOpen);

    //displays or hides the menu depending on the menu open boolean
    if(navOpen){
        navToggler.innerHTML = '&#10005;';
        navList.style.display = 'block';
        navList.classList.add('text-center')
        navList.innerHTML =
        '<ul class="row">'
        + '<li class="col-sm-12"><a href="/index.html" id="active">Home</a></li>'
        + '<li class="col-sm-12"><a href="/resume.html" >Resume</a></li>'
        + '<li class="col-sm-12"><a href="/projects.html">Projects</a></li>'
        + '<li class="col-sm-12"><a href="/links.html">Links</a></li>'
        + '</ul>'
        + '<a href="https://www.linkedin.com/in/gene-cathcart-49848a25a/" target="_blank"><i class="fa-brands fa-linkedin"></i></a>'
        + '<a href="https://github.com/Ecthcrt3" target="_blank"><i class="fa-brands fa-github"></i></a>'
    }   
    else{
        navToggler.innerHTML = '&#9776;';
        navList.style.display = 'none';
        navList.classList.remove('text-center')
    } 
}