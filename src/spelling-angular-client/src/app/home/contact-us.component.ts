import { Component, OnInit } from '@angular/core';
import {
    PublicClient,
    SendEmailRequestDto
} from '../core/services/clients';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
    selector: 'app-contactus',
    templateUrl: 'contact-us.component.html'
})

export class ContactUsComponent implements OnInit {

    public contactForm = this.fb.group({
        name: ['', Validators.required],
        email: ['', [Validators.required, Validators.email] ],
        telephone: ['', Validators.required],
        message: ['', Validators.required]
      });

    emailSent = false;

    constructor(private readonly publicClient: PublicClient,
        private fb: FormBuilder
        ) { }

    ngOnInit() {
    }

    sendEmail() {
        const dto: SendEmailRequestDto = new SendEmailRequestDto();
        dto.name = this.contactForm.value.name;
        dto.email = this.contactForm.value.email;
        dto.telephone = this.contactForm.value.telephone;
        dto.message = this.contactForm.value.message;

        this.publicClient.sendEmail(dto)
        .subscribe(
            () => {
                this.emailSent = true;
            }
        );
    }
}
