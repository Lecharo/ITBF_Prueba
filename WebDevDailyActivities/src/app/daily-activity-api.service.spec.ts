import { TestBed } from '@angular/core/testing';

import { DailyActivityApiService } from './daily-activity-api.service';

describe('DailyActivityApiService', () => {
  let service: DailyActivityApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DailyActivityApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
