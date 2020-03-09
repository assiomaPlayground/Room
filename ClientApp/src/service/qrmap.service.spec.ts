import { TestBed } from '@angular/core/testing';

import { QrmapService } from './qrmap.service';

describe('QrmapService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: QrmapService = TestBed.get(QrmapService);
    expect(service).toBeTruthy();
  });
});
