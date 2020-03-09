import { TestBed } from '@angular/core/testing';

import { Abstractservice } from './abstractservice.service';
import { User } from 'src/model/user';

describe('AbstractserviceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: Abstractservice<User> = TestBed.get(Abstractservice);
    expect(service).toBeTruthy();
  });
});
