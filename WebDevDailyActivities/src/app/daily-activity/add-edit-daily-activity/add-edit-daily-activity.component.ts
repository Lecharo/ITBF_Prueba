import { Component, OnInit, Input } from '@angular/core';
import { Observable, of } from 'rxjs';
import { DailyActivityApiService } from 'src/app/daily-activity-api.service';

@Component({
  selector: 'app-add-edit-daily-activity',
  templateUrl: './add-edit-daily-activity.component.html',
  styleUrls: ['./add-edit-daily-activity.component.css']
})
export class AddEditDailyActivityComponent implements OnInit {

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

  @Input() dailyActivity: any;

  id: number = 0;
  employeeId!: number;
  activityId!: number;
  laborId!: number;
  workDay: string = "";//new Date().toISOString().substring(0, 10
  durationLabor: number = 0;
  comments: string = "";

  ngOnInit(): void {
    this.id = this.dailyActivity.id;
    this.employeeId = this.dailyActivity.employeeId;
    this.activityId = this.dailyActivity.activityId;
    this.laborId = this.dailyActivity.laborId;
    this.workDay = this.dailyActivity.workDay.substring(0, 10);
    this.durationLabor = this.dailyActivity.durationLabor;
    this.comments = this.dailyActivity.comments;

    this.service.getEmployeeList().subscribe((data: any) =>
      {
        this.employeesList$ = of(data.result)
      });

    this.service.getActivityList().subscribe((data: any) =>
      {
        this.activitiesList$ = of(data.result)
      });

    this.service.getLaborList().subscribe((data: any) =>
      {
        this.laborsList$ = of(data.result)
      });
  }

  addDailyActivity(){
    var dailyActivity = {
      employeeId: this.employeeId,
      activityId: this.activityId,
      laborId: this.laborId,
      workDay: this.workDay,
      durationLabor: this.durationLabor,
      comments: this.comments
    }
    if(this.employeeId!=null && this.activityId!=null && this.laborId!=null && this.workDay!=null && this.durationLabor!=null ){
      this.service.addDailyActivity(dailyActivity).subscribe(res => {
        var closeModalBtn = document.getElementById('add-edit-modal-close');
        if(closeModalBtn) {
          closeModalBtn.click();
        }

        var showAddSuccess = document.getElementById('add-success-alert');
        if (showAddSuccess) {
          showAddSuccess.style.display = "block";
        }
        setTimeout(function() {
          if(showAddSuccess) {
            showAddSuccess.style.display = "none";
          }
        }, 4000);
      })
    }
    else {
      confirm('Please, input data in all fields');
    }
  }

  updateDailyActivity(){
    var dailyActivity = {
      id: this.id,
      employeeId: this.employeeId,
      activityId: this.activityId,
      laborId: this.laborId,
      workDay: this.workDay,
      durationLabor: this.durationLabor,
      comments: this.comments
    }

    var id:number = this.id;

    this.service.updateDailyActivity(id, dailyActivity).subscribe(res => {
      var closeModalBtn = document.getElementById('add-edit-modal-close');
      if(closeModalBtn) {
        closeModalBtn.click();
      }

      var showUpdateSuccess = document.getElementById('update-success-alert');
      if (showUpdateSuccess) {
        showUpdateSuccess.style.display = "block";
      }
      setTimeout(function() {
        if(showUpdateSuccess) {
          showUpdateSuccess.style.display = "none";
        }
      }, 4000);
    })
  }

}
