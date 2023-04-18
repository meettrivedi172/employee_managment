import { Component, OnInit } from '@angular/core';
import { FormGroup,FormControl ,Validators} from '@angular/forms';
import { EmployeeService } from '../employee.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit{
genders = null  ;
department=null;
baseUrl ="http://localhost:5029/api/Employee/search=";
employeeForm !:FormGroup;
employeeList:any;
editmode=false;
employeeEditForm !:FormGroup;
id:number =0;
searchvalue=null;

editformvalue:any;
  constructor(private emplyoyeeservice:EmployeeService,private http:HttpClient){}

  ngOnInit(): void {
   this.getemployeeList()

    this.employeeForm= new FormGroup({
      email: new FormControl('',Validators.required),
      name: new FormControl('',Validators.required),
      dob: new FormControl('',Validators.required),
      department: new FormControl('',Validators.required),
      gender: new FormControl('',Validators.required),

    });

    this.employeeEditForm= new FormGroup({
      id: new FormControl('',Validators.required),
      email: new FormControl('',Validators.required),
      name: new FormControl('',Validators.required),
      dob: new FormControl('',Validators.required),
      department: new FormControl('',Validators.required),
      gender: new FormControl('',Validators.required),

    });

  }



  addEmployee(){
    console.log(this.employeeForm.value)
    this.emplyoyeeservice.addEmployee(this.employeeForm.value).subscribe(res=>{

      this.getemployeeList();
    })
    this.employeeForm.reset();

  }

  getemployeeList(){

    this.emplyoyeeservice.getEmployees(this.genders,this.department).subscribe(res=>{
      console.log(res);
      this.employeeList= res;
    })


  }

  applyfilter(){
    this.emplyoyeeservice.applyFilter(this.genders,this.department).subscribe(res=>{
      console.log(res);
      this.employeeList= res;
    })
  }

  RemoveFilter(){

    this.genders = null  ;
    this.department=null;
    this.searchvalue = null;
    this.getemployeeList();

  }


  deleteemployee(id:number){

    var box=confirm("are you sure delete this employee detail?");
    console.log(box);
    if(box){

      this.emplyoyeeservice.deleteEmployee(id).subscribe(res=>{

        this.getemployeeList()
      });
    }else{

      console.log(id);
    }


  }

  editEmplyoyeedata(id:number){
    this.id = id;

    this.emplyoyeeservice.singalEmployeeDetail(id).subscribe(res=>{
      console.log(res);
      this.editformvalue=res;
      this.employeeEditForm.patchValue(this.editformvalue);
    })




  }


  updateEmplyoyeedata(){

    this.emplyoyeeservice.updateEmployee(this.employeeEditForm.value.id,this.employeeEditForm.value).subscribe(res=>{
       console.log(res);
       this.getemployeeList()

    })



  }


  serchVal(){

     if(this.searchvalue==null){
      this.getemployeeList();
     }else{

       this.http.get(this.baseUrl+this.searchvalue).subscribe(res=>{
        this.employeeList= res;
       })
     }

  }
}
