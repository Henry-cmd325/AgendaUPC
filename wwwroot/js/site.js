const validateSignin = () => {
    let isValid = true;
    let $form = document.getElementById("form-login");

    let spansList = document.getElementsByClassName("val-error");

    for(let span of spansList){
        span.innerText = "";
    }

    if (document.getElementById("nombre").value === ""){
        document.getElementById("nombre-error").innerText = "Debe de rellenar el campo nombre";
        isValid = false;
    }
    
    if (document.getElementById("matricula").value === ""){
        document.getElementById("matricula-error").innerText = "Debe de rellenar el campo matricula";
        isValid = false;
    }
       
    if (document.getElementById("password-1").value === ""){
        document.getElementById("password-1-error").innerText = "Debe de rellenar el campo contraseña";
        isValid = false;
    }

    if (document.getElementById("password-2").value !== document.getElementById("password-1").value){
        document.getElementById("password-2-error").innerText = "Las dos contraseñas deben de coincidir";
        isValid = false;
    }

    if (isValid) $form.submit();  
};

const validateLogin = () => {
    let isValid = true;
    let $form = document.getElementById("form-login");

    let spansList = document.getElementsByClassName("val-error");

    for (let span of spansList) {
        span.innerText = "";
    }

    if (document.getElementById("input-name").value === ""){
        document.getElementById("input-name-error").innerText = "Debe de rellenar el campo nombre";
        isValid = false;
    }

    if (document.getElementById("input-password").value === ""){
        document.getElementById("input-password-error").innerText = "Debe de rellenar el campo contraseña";
        isValid = false;
    }

    if (isValid) $form.submit();
};

const validateCreateHomework = () => {
    let isValid = true;
    let $form = document.getElementById("form-create-hw");

    let spansList = document.getElementsByClassName("val-error");

    for (let span of spansList){
        span.innerText = "";
    }

    if (document.getElementById("nombre").value === ""){
        document.getElementById("nombre-error").innerText = "Debe de rellenar el campo nombre";
        isValid = false;
    }

    if (document.getElementById("materia").value === null){
        document.getElementById("materia-error").innerText = "Debes de seleccionar una materia";
        isValid = false;
    }

    if (document.getElementById("descripcion").value === null){
        document.getElementById("error-descripcion").innerText = "Debes de escribir una descripción para la tarea";
        isValid = false;
    }

    if (document.getElementById("fecha-limite").value === null){
        document.getElementById("error-date").innerText = "Debes de seleccionar una fecha";
        isValid = false;
    }

    if (isValid) $form.submit();
};
