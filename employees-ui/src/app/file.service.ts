import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class FileService {
  private responseData: any[] = [];
  constructor(private http: HttpClient) { }

  readAndSendFile(file: File): Promise<void> {
    console.log("Reading file");
    return new Promise<void>((resolve, reject) => {
      const reader = new FileReader();
      reader.onload = (event) => {
        const content = event.target?.result as string;
        this.sendContentToApi(content).subscribe(
          (response: any) => {
            this.responseData=response;
            resolve();
          },
          (error) => reject(error)
        );
      };
      reader.onerror = (error) => reject(error);
      reader.readAsText(file);
    });
  }

  private sendContentToApi(content: string) {
    const apiUrl = 'https://localhost:7109/Employee';
    return this.http.post(apiUrl, { content });
  }
}
