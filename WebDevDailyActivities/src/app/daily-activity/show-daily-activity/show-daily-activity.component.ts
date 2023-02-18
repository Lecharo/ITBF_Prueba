import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { DailyActivityApiService } from 'src/app/daily-activity-api.service';

@Component({
  selector: 'app-show-daily-activity',
  templateUrl: './show-daily-activity.component.html',
  styleUrls: ['./show-daily-activity.component.css']
})
export class ShowDailyActivityComponent implements OnInit {

  dailyActivityList$!: Observable<any[]>;

  employeesList$!: Observable<any[]>;
  employeesList: any[] = [];

  activitiesList$!: Observable<any[]>;
  activitiesList: any[] = [];

  laborsList$!: Observable<any[]>;
  laborsList: any[] = [];

  //Map to display data associates with the foreign keys
  employeesMap:Map<number, string> = new Map();
  activitiesMap:Map<number, string> = new Map();
  laborsMap:Map<number, string> = new Map();

  constructor(private service: DailyActivityApiService) { }

  ngOnInit(): void {
    this.service.getDailyActivityList().subscribe((data: any) => {
        this.dailyActivityList$ = of(data.result)
        console.log(data);
      });

      this.refreshEmployeeMap();
      this.refreshActivityMap();
      this.refreshLaborMap();

    }

    refreshEmployeeMap() {
      this.service.getEmployeeList().subscribe((data: any) =>
        {
          this.employeesList$ = of(data.result)
          this.employeesList = data.result;
          for(let i = 0; i < data.result.length; i++) {
            this.employeesMap.set(data.result[i].id, data.result[i].employeeName);
          }
       });
    }

    refreshActivityMap() {
      this.service.getActivityList().subscribe((data: any) =>
        {
          this.activitiesList$ = of(data.result)
          for(let i = 0; i < data.result.length; i++) {
            this.activitiesMap.set(data.result[i].id, data.result[i].activityName);
          }
       });
    }

    refreshLaborMap() {
      this.service.getLaborList().subscribe((data: any) =>
        {
          this.laborsList$ = of(data.result)

          for(let i = 0; i < data.result.length; i++) {
            this.laborsMap.set(data.result[i].id, data.result[i].laborName);
          }
       });
    }

    // Variables (properties) to hold the values of the selected item
    modalTitle: string = "";
    activateAddEditDailyActivityComp: boolean = false;
    dailyActivity: any;

    // Method to add a new daily activity
    modalAdd() {
      this.dailyActivity = {
        id: 0,
        employeeId: 0,
        activityId: 0,
        laborId: 0,
        workDay: new Date().toISOString().slice(0, 10),
        durationLabor: 0,
        comments: ""
      }
      this.modalTitle = "Add Daily Activity";
      this.activateAddEditDailyActivityComp = true;
    }

    // Method to delete an existing daily activity
    delete(item:any) {
      if(confirm(`Are you sure you want to delete Daily Activity ${ item.id} ?`)) {
        this.service.deleteDailyActivity(item.id).subscribe(res => {
          var closeModalBtn = document.getElementById('add-edit-modal-close');
          if(closeModalBtn) {
            closeModalBtn.click();
          }

          var showDeleteSuccess = document.getElementById('delete-success-alert');
          if(showDeleteSuccess) {
            showDeleteSuccess.style.display = "block";
          }
          setTimeout(function() {
            if(showDeleteSuccess) {
              showDeleteSuccess.style.display = "none";
            }
          }, 4000);
        })
      }
    }

    modalClose() {
      this.activateAddEditDailyActivityComp = false;

      this.service.getDailyActivityList().subscribe((data: any) => {
        this.dailyActivityList$ = of(data.result)
        console.log(data);
      });

      this.refreshEmployeeMap();
      this.refreshActivityMap();
      this.refreshLaborMap();

    }

    modalEdit(item:any) {
      this.dailyActivity = item;
      this.modalTitle = "Edit Daily Activity";
      this.activateAddEditDailyActivityComp = true;
    }
  }
