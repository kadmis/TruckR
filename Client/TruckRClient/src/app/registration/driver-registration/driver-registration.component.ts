import { Component, OnInit, HostListener } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RegisterDriverService } from 'src/infrastructure/methods/register-driver/register-driver.service';
import { RegisterDriverModel } from 'src/infrastructure/methods/register-driver/register-driver-model';
import { LoaderService } from 'src/infrastructure/common/loader.service';
import { NotificationService } from 'src/infrastructure/common/notification.service';

@Component({
  selector: 'app-driver-registration',
  templateUrl: './driver-registration.component.html',
  styleUrls: ['./driver-registration.component.scss']
})
export class DriverRegistrationComponent implements OnInit {
  focusFirstname: boolean;
  focusLastname: boolean;
  focusEmail: boolean;
  focusPhone: boolean;
  focusLogin: boolean;
  focusPassword: boolean;

  title: string = "Kierowca";

  constructor(
    private formBuilder: FormBuilder,
    private registerDriver: RegisterDriverService,
    private loader: LoaderService,
    private notification: NotificationService) {
     }

  ngOnInit(): void {
    this.initForm();
    this.initStyling();
  }

  ngOnDestroy():void {
    this.destroyStyling();
  }

  registrationForm: FormGroup;

  initForm = ():void => {
    this.registrationForm = this.formBuilder.group({
      firstname: ["", [Validators.required]],
      lastname: ["", [Validators.required]],
      email: ["", [Validators.required, Validators.email]],
      phone: ["", Validators.required],
      login: ["", [Validators.required, Validators.pattern("^(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$")]],
      password: ["", [Validators.required, Validators.pattern("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]]
    });
  }

  confirm = ():void => {
    this.loader.show("Rejestrowanie...");
    let firstname = this.registrationForm.controls["firstname"].value;
    let lastname = this.registrationForm.controls["lastname"].value;
    let email = this.registrationForm.controls["email"].value;
    let phone = this.registrationForm.controls["phone"].value;
    let login = this.registrationForm.controls["login"].value;
    let password = this.registrationForm.controls["password"].value;

    let model = new RegisterDriverModel(firstname, lastname, email, phone, login, password);

    this.registerDriver.register(model).subscribe((res)=>{
      if(res.successful) {
        this.notification.showSuccess("Rejestracja kierowcy powiodła się. Link aktywacyjny został przesłany na podany email.");
      } else {
        this.notification.showError("Rejestracja nie powiodła się.");
      }
      this.loader.hide();
    });
  }

  @HostListener("document:mousemove", ["$event"])
  onMouseMove(e) {
    if(e){
      var squares1 = document.getElementById("square1");
      var squares2 = document.getElementById("square2");
      var squares3 = document.getElementById("square3");
      var squares4 = document.getElementById("square4");
      var squares5 = document.getElementById("square5");
      var squares6 = document.getElementById("square6");
      var squares7 = document.getElementById("square7");
      var squares8 = document.getElementById("square8");
  
      var posX = e.clientX - window.innerWidth / 2;
      var posY = e.clientY - window.innerWidth / 6;
  
      squares1.style.transform =
        "perspective(500px) rotateY(" +
        posX * 0.05 +
        "deg) rotateX(" +
        posY * -0.05 +
        "deg)";
      squares2.style.transform =
        "perspective(500px) rotateY(" +
        posX * 0.05 +
        "deg) rotateX(" +
        posY * -0.05 +
        "deg)";
      squares3.style.transform =
        "perspective(500px) rotateY(" +
        posX * 0.05 +
        "deg) rotateX(" +
        posY * -0.05 +
        "deg)";
      squares4.style.transform =
        "perspective(500px) rotateY(" +
        posX * 0.05 +
        "deg) rotateX(" +
        posY * -0.05 +
        "deg)";
      squares5.style.transform =
        "perspective(500px) rotateY(" +
        posX * 0.05 +
        "deg) rotateX(" +
        posY * -0.05 +
        "deg)";
      squares6.style.transform =
        "perspective(500px) rotateY(" +
        posX * 0.05 +
        "deg) rotateX(" +
        posY * -0.05 +
        "deg)";
      squares7.style.transform =
        "perspective(500px) rotateY(" +
        posX * 0.02 +
        "deg) rotateX(" +
        posY * -0.02 +
        "deg)";
      squares8.style.transform =
        "perspective(500px) rotateY(" +
        posX * 0.02 +
        "deg) rotateX(" +
        posY * -0.02 +
        "deg)";
    }
  }

  initStyling = ():void => {
    var body = document.getElementsByTagName("body")[0];
    body.classList.add("register-page");

    this.onMouseMove(event);
  }

  destroyStyling = ():void => {
    var body = document.getElementsByTagName("body")[0];
    body.classList.remove("register-page");
  }
}
