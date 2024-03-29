import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Employee } from './employee-list/employee-list.component';

@Injectable({
  providedIn: 'root'
})
export class FileService {
  private responseData: Employee;
  constructor(private http: HttpClient) { }

  readAndSendFile(file: File): Promise<Employee> {
    console.log("Reading file");
    return new Promise<Employee>((resolve, reject) => {
      const reader = new FileReader();
      reader.onload = (event) => {
        const content = event.target?.result as string;
        this.sendContentToApi(content).subscribe(
          (response: any) => {
            this.responseData=response;
            resolve(response);
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
