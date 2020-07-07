import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Photo } from '../../_models/photo';
import { FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment';
import { AuthService } from 'src/app/_services/auth.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-user-photos',
  templateUrl: './user-photos.component.html',
  styleUrls: ['./user-photos.component.css']
})
export class UserPhotosComponent implements OnInit {

  @Input() photos: Photo[];
  @Output() getUserPhotoChange = new EventEmitter<string>();
 
  uploader:FileUploader;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;
  currentMain: Photo;
  

  constructor(private authService: AuthService,
              private userService: UserService,
              private alertify: AlertifyService) { }

  ngOnInit(): void {
    this.initializeUploader();

  }

  public fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'users/' + this.authService.decodedToken.nameid + '/photos',
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });

    this.uploader.onAfterAddingFile = (file) => { file.withCredentials = false; };

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const res: Photo = JSON.parse(response);
        const photo = {
          id: res.id,
          url: res.url,
          dateAdded: res.dateAdded,
          description: res.description,
          mainPhoto: res.mainPhoto
        };
        this.photos.push(photo);
        if(photo.mainPhoto){
          this.authService.changeUserPhoto(photo.url);
          this.authService.currentUser.photoUrl = photo.url;
          localStorage.setItem('user', JSON.stringify(this.authService.currentUser));
         }
      }
    };

  }


  setMainPhoto(photo: Photo) {
    this.userService.SetMainPhoto(this.authService.decodedToken.nameid, photo.id).subscribe(() => {
      console.log('Sukces, zdjęcie ustawione jako główne');
      if (this.currentMain = this.photos.filter(p => p.mainPhoto === true)[0]) {
        this.currentMain.mainPhoto = false;
      }
      photo.mainPhoto = true;
      this.authService.changeUserPhoto(photo.url);
      this.authService.currentUser.photoUrl = photo.url;
      localStorage.setItem('user', JSON.stringify(this.authService.currentUser));
    }, error => {
      this.alertify.error(error);
    });
  }

  deletePhoto(id: number) {
    this.alertify.confirm('Czy jesteś pewien że chcesz usunąć to zdjęcie', () => {
      this.userService.DeletePhoto(this.authService.decodedToken.nameid, id).subscribe(() => {
         this.photos.splice(this.photos.findIndex(p => p.id === id), 1);
         this.alertify.success("Zdjęcie zostało usunięte");
      }, error => {
        this.alertify.error("Nie udało się usunąć zdjęcia");
      })
    });
  }




}


