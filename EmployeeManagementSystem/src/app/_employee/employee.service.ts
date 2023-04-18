import { Injectable } from '@angular/core';
import { Employee } from './employee';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  baseUrl= "http://localhost:5029/api/"
  constructor(private http:HttpClient) { }


  addEmployee(employee: Employee): Observable<Employee> {
    return this.http.post<Employee>(this.baseUrl+"employee/add-employee", employee);
  }


  getEmployeeList(){

    return this.http.get(this.baseUrl+"employee")

  }



  deleteEmployee(id: number): Observable<any> {
    // const url = `${this.baseUrl}/${id}`;
    return this.http.delete(this.baseUrl+"employee/"+id);
}
singalEmployeeDetail(id:number){
    return this.http.get(this.baseUrl+"employee/"+id)
}

updateEmployee(id: number, employee: Employee): Observable<any> {
  const url = this.baseUrl + "employee/" + id;
  return this.http.put(url, employee);
}

applyFilter(gender?: any, department?: any){
  let url = `${this.baseUrl}employee/filter?`;

  if (gender != null) {
    url += `gender=${gender}&`;
  }

  if (department != null) {
    url += `department=${department}&`;
  }



  return this.http.get<any>(url);
}

getEmployees(gender?: any, department?: any): Observable<Employee[]> {
  let url = `${this.baseUrl}employee`;




  return this.http.get<any>(url);
}
}
