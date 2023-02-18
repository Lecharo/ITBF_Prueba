import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DailyActivityApiService {
  readonly APIUrl = "https://localhost:7111/api";
  constructor(private http:HttpClient) { }

  // Llamado a la API para obtener todos los registros de la tabla DailyActivity
  getDailyActivityList(): Observable<any[]> {
    return this.http.get<any>(this.APIUrl + '/DailyActivities');
  }

  addDailyActivity(data: any) {
    return this.http.post(this.APIUrl + '/DailyActivities', data);
  }

  updateDailyActivity(id: number | string, data: any) {
    return this.http.put(this.APIUrl + `/DailyActivities/${id}`, data);
  }

  deleteDailyActivity(id: number | string) {
    return this.http.delete(this.APIUrl + `/DailyActivities/${id}`);
  }

    // Llamado a la API para obtener todos los registros de la tabla Employee
    getEmployeeList(): Observable<any[]> {
      return this.http.get<any>(this.APIUrl + '/Employees');
    }

    // addEmployee(data: any) {
    //   return this.http.post(this.APIUrl + '/Employees', data);
    // }

    // updateEmployee(id: number | string, data: any) {
    //   return this.http.put(this.APIUrl + `/Employees/${id}`, data);
    // }

    // deleteEmployee(id: number | string) {
    //   return this.http.delete(this.APIUrl + `/Employees/${id}`);
    // }

    // Llamado a la API para obtener todos los registros de la tabla Activity
    getActivityList(): Observable<any[]> {
      return this.http.get<any>(this.APIUrl + '/Activities');
    }

    // addActivity(data: any) {
    //   return this.http.post(this.APIUrl + '/Activities', data);
    // }

    // updateActivity(id: number | string, data: any) {
    //   return this.http.put(this.APIUrl + `/Activities/${id}`, data);
    // }

    // deleteActivity(id: number | string) {
    //   return this.http.delete(this.APIUrl + `/Activities/${id}`);
    // }

      // Llamado a la API para obtener todos los registros de la tabla Labor
  getLaborList(): Observable<any[]> {
    return this.http.get<any>(this.APIUrl + '/Labors');
  }

  // addLabor(data: any) {
  //   return this.http.post(this.APIUrl + '/Labors', data);
  // }

  // updateLabor(id: number | string, data: any) {
  //   return this.http.put(this.APIUrl + `/Labors/${id}`, data);
  // }

  // deleteLabor(id: number | string) {
  //   return this.http.delete(this.APIUrl + `/Labors/${id}`);
  // }

  }
