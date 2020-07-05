import { TestBed } from '@angular/core/testing';

import { ServerexceptionproviderService } from './serverexceptionprovider.service';

describe('ServerexceptionproviderService', () => {
  let service: ServerexceptionproviderService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServerexceptionproviderService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
