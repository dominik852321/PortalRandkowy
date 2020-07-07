import { Component, OnInit, Input } from '@angular/core';
import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { UserService } from 'src/app/_services/user.service';
import { AuthService } from 'src/app/_services/auth.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-user-messages',
  templateUrl: './user-messages.component.html',
  styleUrls: ['./user-messages.component.css']
})
export class UserMessagesComponent implements OnInit {

  @Input() recipientId: number;
  messages: Message[];


  constructor(private userService: UserService,
              private authService: AuthService,
              private alertify: AlertifyService
              ) { }

  ngOnInit(): void {
    this.loadMessages();
  }

  loadMessages() {
    this.userService.GetMessageThread(this.authService.decodedToken.nameid, this.recipientId)
    .subscribe(messages => {
       this.messages= messages;
    }, error => {
      this.alertify.error(error);
    });
  }



}
