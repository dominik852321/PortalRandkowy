import { Component, OnInit } from '@angular/core';
import { Message } from 'src/app/_models/message';
import { Pagination, PaginationResult } from '../_models/pagination';
import { UserService } from '../_services/user.service';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {

  messages: Message[];
  pagination: Pagination;
  messageContainer = 'Nieprzeczytane';
  flagaOutbox =false;


  constructor(private userService: UserService,
              private authService: AuthService,
              private alertify: AlertifyService,
              private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.messages = data.messages.result;
      this.pagination = data.messages.pagination;
    });
  }


  loadMessages() {
    this.userService.GetMessages(this.authService.decodedToken.nameid, this.pagination.currentPage,
                                 this.pagination.itemsPerPage, this.messageContainer )
       .subscribe((res: PaginationResult<Message[]>) => {
         this.messages = res.result;
         this.pagination = res.pagination;
        
         if(res.result[0].messageContainer === 'Outbox') {
           this.flagaOutbox = true;
         }
         else 
         {
           this.flagaOutbox =false;
         }
    }, error => {
      this.alertify.error(error);
    });                             
  }

  deleteMessage(messageId: number){
    this.alertify.confirm('Czy napewno chcesz usunąć tę wiadomość?', () => {
      this.userService.DeleteMessage(this.authService.decodedToken.nameid, messageId).subscribe(() => {
        this.messages.splice(this.messages.findIndex(m => m.id === messageId), 1);
        this.alertify.success('Wiadomość została usunięta');
      }, error => {
        this.alertify.error('Nie udało się usunąć wiadomości');
      });
    });
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadMessages();
  }

  

}
