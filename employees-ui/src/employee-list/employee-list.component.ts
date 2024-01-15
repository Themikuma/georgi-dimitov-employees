import { Component, ViewChild } from '@angular/core';
import { MatTable, MatTableModule } from '@angular/material/table';
import { FileService } from 'src/file.service';

export interface Employee {
  firstEmpId: number;
  secondEmpId: number;
  timeInDays: number;
}

const employeeData: Employee[] = [];

@Component({
  selector: 'employee-list',
  styleUrls: ['employee-list.component.css'],
  templateUrl: 'employee-list.component.html',
  standalone: true,
  imports: [MatTableModule],
})
export class EmployeeTableComponent {

  @ViewChild(MatTable) table: MatTable<Employee>;
  dataSource = [...employeeData];
  constructor(private fileService: FileService) { }
  displayedColumns: string[] = ['firstEmpId', 'secondEmpId', 'timeInDays'];

  async onFileSelected(event: any) {
    const file = event.target.files[0] as File;
    if (file) {
      const t = await this.fileService.readAndSendFile(file);
      console.log(t);
      this.dataSource.length = 0;
      this.dataSource.push(t);
      this.table.renderRows();
    }
  }
}