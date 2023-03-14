const validateSignin = () => {
    let isValid = true;
    let $form = document.getElementById("form-login");

    let spansList = document.getElementsByClassName("val-error");

    spansList.forEach(span => span.innerText = "");

    if (document.getElementById("nombre").value === ""){
        document.getElementById("nombre-error").innerText = "Debe de rellenar el campo nombre";
        isValid = false;
    }
    
    if (document.getElementById("matricula").value === ""){
        document.getElementById("metricula-error").innerText = "Debe de rellenar el campo matricula";
        isValid = false;
    }
       
    if (document.getElementById("password-1").value === ""){
        document.getElementById("pasword-1-error").innerText = "Debe de rellenar el campo contraseña";
        isValid = false;
    }

    if (document.getElementById("password-2").value !== document.getElementById("password-1").value){
        document.getElementById("password-2-error").innerText = "Las dos contraseñas deben de coincidir";
        isValid = false;
    }

    if (isValid)
        $form.submit();  
};

const validateLogin = () => {
    $form = document.getElementById("form-login");
};