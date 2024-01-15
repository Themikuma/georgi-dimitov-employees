import { Component } from '@angular/core';
import { FileService } from '../../file.service';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css']
})
export class FileUploadComponent {
  constructor(private fileService: FileService) {}

  onFileSelected(event: any) {
    const file = event.target.files[0] as File;
    if (file) {
      this.fileService.readAndSendFile(file).then(
        () => console.log('File sent successfully'),
        (error) => console.error('Error sending file:', error)
      );
    }
  }
}
