import { TestBed } from '@angular/core/testing';

import { AlertserviceService } from './alertservice.service';

describe('AlertserviceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AlertserviceService = TestBed.get(AlertserviceService);
    expect(service).toBeTruthy();
  });
});
